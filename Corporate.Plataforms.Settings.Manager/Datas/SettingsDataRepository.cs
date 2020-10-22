using AspNetCore.EntityFramework.Procedure.Tools;
using Corporate.Plataforms.Settings.Manager.Datas.Interfaces;
using Corporate.Plataforms.Settings.Manager.Entities;
using Corporate.Plataforms.Settings.Manager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Corporate.Plataforms.Settings.Manager.Datas
{
    public class SettingsDataRepository : ISettingsDataRepository
    {
        private readonly SettingsDataContext _context;

        public SettingsDataRepository(SettingsDataContext context)
        {
            _context = context;
        }


        public List<InstanceEntity> ListInstances()
        {
            return _context.GetInstances.FromSqlRaw("GET_INSTANCES").ToList();
        }

        public List<PropertyDataEntity> ListPropertiesData()
        {
            return _context.GetPropertiesData.FromSqlRaw("GET_INSTANCE_PROPERTY_VALUES",
                new SqlParameter("@INSTANCE_NAME", SqlDbType.VarChar) { Value = DBNull.Value }).ToList();
        }

        public List<PropertyDataEntity> GetInstancePropertiesData(string instanceName)
        {
            return _context.GetPropertiesData.FromSqlRaw("GET_INSTANCE_PROPERTY_VALUES",
                new SqlParameter("@INSTANCE_NAME", SqlDbType.VarChar) { Value = instanceName }).ToList();
        }

        public TransactionCommandResult UpdateInstancePropertyData(string instanceName, PropertyDataUpdate propertyData)
        {
            var propertyIdParameter = new SqlParameter("@PROPERTY_ID", SqlDbType.Int) { Direction = ParameterDirection.Output };

            return _context.GetTransactionCommandResult.FromSql("UPDATE_INSTANCE_PROPERTY_VALUE",
                        new SqlParameter("@INSTANCE_NAME", SqlDbType.VarChar) { Value = instanceName },
                        new SqlParameter("@PROPERTY_NAME", SqlDbType.VarChar) { Value = propertyData.PropertyName },
                        new SqlParameter("@SETTING_REFERENCE", SqlDbType.VarChar) { Value = propertyData.SettingReference },
                        new SqlParameter("@PROPERTY_VALUE", SqlDbType.VarChar) { Value = propertyData.PropertyValue },
                        propertyIdParameter
                ).FirstOrDefault();
        }

        public List<IntegrationEntity> GetIntegrations()
        {
            throw new NotImplementedException();
        }

        public List<IntegrationEntity> GetIntegrations(int propertyId)
        {
            throw new NotImplementedException();
        }

    }
}
