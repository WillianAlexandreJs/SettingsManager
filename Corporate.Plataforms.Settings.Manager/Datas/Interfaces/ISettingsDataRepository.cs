using AspNetCore.EntityFramework.Procedure.Tools;
using Corporate.Plataforms.Settings.Manager.Entities;
using Corporate.Plataforms.Settings.Manager.Models;
using System.Collections.Generic;

namespace Corporate.Plataforms.Settings.Manager.Datas.Interfaces
{
    public interface ISettingsDataRepository
    {
        TransactionCommandResult UpdateInstancePropertyData(string instanceName, PropertyDataUpdate propertyData);

        List<IntegrationEntity> GetIntegrations(int propertyId);

        List<PropertyDataEntity> GetInstancePropertiesData(string instanceName);
    }
}
