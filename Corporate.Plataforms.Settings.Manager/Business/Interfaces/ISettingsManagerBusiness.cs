using Corporate.Plataforms.Settings.Manager.Models;
using Corporate.Plataforms.Settings.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager.Business.Interfaces
{
    public interface ISettingsManagerBusiness
    {
        Task UpdatePropertyValue(string instanceName, PropertyDataUpdate propertyValue);

        Task<List<PropertyData>> GetInstanceSettings(string instanceName);

    }
}
