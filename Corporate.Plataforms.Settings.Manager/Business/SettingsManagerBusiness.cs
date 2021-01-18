using AspNetCore.DistributedCache.Tools;
using AutoMapper;
using Corporate.Plataforms.Settings.Manager.Business.Interfaces;
using Corporate.Plataforms.Settings.Manager.Datas.Interfaces;
using Corporate.Plataforms.Settings.Manager.Entities;
using Corporate.Plataforms.Settings.Manager.Models;
using Corporate.Plataforms.Settings.Manager.Services.Interfaces;
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
        private readonly IAppConfigurationService _appConfigurationBusiness;
        private readonly IMapper _mapper;

        public SettingsManagerBusiness(IHubContext<SettingsHubClient> settingsHubContext, 
                                        IDistributedCacheRepository distributedCache, 
                                        ISettingsDataRepository settingsData,
                                        IAppConfigurationService appConfigurationBusiness,
                                        IMapper mapper)
        {
            _settingsHubContext = settingsHubContext;
            _distributedCache = distributedCache;
            _settingsData = settingsData;
            _appConfigurationBusiness = appConfigurationBusiness;
            _mapper = mapper;
        }


        public async Task UpdatePropertyValue(string instanceName, PropertyDataUpdate propertyValue)
        {
            var propertyUpdated = _settingsData.UpdateInstancePropertyData(instanceName, propertyValue);

            if (propertyUpdated.ErrorNumber > 0)
                throw new Exception(propertyUpdated.ErrorMessage);

            await SendInstanceUpdateInstanceSettings(instanceName, propertyValue);

        }

        public async Task<List<PropertyData>> GetInstanceSettings(string instanceName)
        {
            return await GetCacheIsNullRaizeSync<List<PropertyData>>(instanceName, () => GetInstanceSettingsDb(instanceName) );
        }

        public async Task<List<PropertyData>> GetInstanceSettingsDb(string instanceName)
        {
            var propertiesDataEntity =  _settingsData.GetInstancePropertiesData(instanceName);
            List<PropertyData> properties = _mapper.Map<List<PropertyData>>(propertiesDataEntity);

            foreach (var property in properties.Where(x => x.PropertyType.Equals("AppConfigurationValue")))
            {
                property.PropertyValue = await _appConfigurationBusiness.GetKeyValue(property.PropertyName, property.InstanceName);
            }

            return properties;
        }

        private  async Task<T> GetCacheIsNullRaizeSync<T>(string instanceName, Func<Task<T>> actionGetDados)
        {
            T data = _distributedCache.GetCache<T>(instanceName);

            if (data is null)
                data = await actionGetDados.Invoke();

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
