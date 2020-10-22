using Corporate.Plataforms.Settings.Manager.Models;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager.Business.Interfaces
{
    public interface ISettingsManagerBusiness
    {
        Task UpdatePropertyValue(string instanceName, PropertyDataUpdate propertyValue);

        Task SendInstanceSettings(string instanceName);

    }
}
