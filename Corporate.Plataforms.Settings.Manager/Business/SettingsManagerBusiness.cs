using AspNetCore.DistributedCache.Tools;
using AutoMapper;
using Corporate.Plataforms.Settings.Manager.Business.Interfaces;
using Corporate.Plataforms.Settings.Manager.Datas.Interfaces;
using Corporate.Plataforms.Settings.Manager.Entities;
using Corporate.Plataforms.Settings.Manager.Models;
using Corporate.Plataforms.Settings.Model;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager.Business
{
    public class SettingsManagerBusiness : ISettingsManagerBusiness
    {
        private readonly IHubContext<SettingsHubClient> _settingsHubContext;
        private readonly IDistributedCacheRepository _distributedCache;
        private readonly ISettingsDataRepository _settingsData;
        private readonly IMapper _mapper;

        public SettingsManagerBusiness(IHubContext<SettingsHubClient> settingsHubContext, 
                                        IDistributedCacheRepository distributedCache, 
                                        ISettingsDataRepository settingsData, 
                                        IMapper mapper)
        {
            _settingsHubContext = settingsHubContext;
            _distributedCache = distributedCache;
            _settingsData = settingsData;
            _mapper = mapper;
        }


        public async Task UpdatePropertyValue(string instanceName, PropertyDataUpdate propertyValue)
        {
            var propertyUpdated = _settingsData.UpdateInstancePropertyData(instanceName, propertyValue);

            if (propertyUpdated.ErrorNumber > 0)
                throw new Exception(propertyUpdated.ErrorMessage);

            await SendInstanceUpdateInstanceSettings(instanceName, propertyValue);

        }

        public async Task SendInstanceSettings(string instanceName)
        {
            List<PropertyData> properties = GetCacheIsNullRaizeSync<List<PropertyData>, List<PropertyDataEntity>>(instanceName, 
                                                                    () => _settingsData.GetInstancePropertiesData(instanceName)
                                            );

            await _settingsHubContext.Clients.User(instanceName).SendAsync("GetInstanceSettings", _mapper.Map<List<PropertyValue>>(properties));
        }

        private T GetCacheIsNullRaizeSync<T, TEntity>(string instanceName, Func<TEntity> actionGetDados)
        {
            T data = _distributedCache.GetCache<T>(instanceName);

            if (data is null)
                data = _mapper.Map<T>(actionGetDados.Invoke());

            //if (!(properties is null) || properties.Any()) 
            //Invoke IBackGroundWoker Update Cache

            return data;
        }

        private async Task SendInstanceUpdateInstanceSettings(string instanceName, PropertyDataUpdate propertyValue)
        {
            await _settingsHubContext.Clients.User(instanceName).SendAsync("UpdateInstanceSettings", propertyValue);
        }
    }
}
