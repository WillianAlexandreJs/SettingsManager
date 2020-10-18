using System.Threading.Tasks;
using Corporate.Plataforms.Settings.Model;

namespace Corporate.Plataforms.Settings.Manager.Interfaces
{
    public interface ISettingsHubClient
    {
        Task HubApplicationConfiguration(PropertyValue itemConfiguracao);
    }
}
