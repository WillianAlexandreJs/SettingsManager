using Corporate.Plataforms.Settings.Manager.Models;
using Corporate.Plataforms.Settings.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager.Interfaces
{
    public interface ISettingsHubClient
    {
        Task UpdateInstanceSettings(PropertyData propertyData);

        Task<List<PropertyData>> GetInstanceSettings(string instanceName);


    }
}
