using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager.Services.Interfaces
{
    public interface IAppConfigurationService
    {
        Task<string> GetKeyValue(string key, string label);
    }
}
