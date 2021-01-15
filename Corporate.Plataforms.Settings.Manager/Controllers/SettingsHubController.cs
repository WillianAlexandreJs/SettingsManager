using Corporate.Plataforms.Settings.Manager.Datas.Interfaces;
using Corporate.Plataforms.Settings.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace Corporate.Plataforms.Settings.Manager.Controllers
{
    [ApiController]
    [Route("api/Settings/[action]")]
    public class SettingsHubController : Controller
    {
        private readonly IHubContext<SettingsHubClient> _settingsHubContext;
        private readonly ISettingsDataRepository _settingsData;

        public SettingsHubController(IHubContext<SettingsHubClient> settingsHubContext, ISettingsDataRepository settingsData)
        {
            _settingsHubContext = settingsHubContext;
            _settingsData = settingsData;
            LoadDictonary();
        }

        private void LoadDictonary()
        {

        }

        [HttpPost]
        public string UpdateGeneralPropertyData([FromBody] PropertyData propertyValue)
        {
            string retMessage;
            try
            {
                _settingsHubContext.Clients.All.SendAsync("UpdateInstanceSettings", propertyValue);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }

        [HttpPost]
        public string UpdateApplicationPropertyData([FromQuery] string applicationName, [FromBody] PropertyData propertyValue)
        {
            string retMessage;
            try
            {
                _settingsHubContext.Clients.Group(applicationName).SendAsync("UpdateInstanceSettings", propertyValue);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }

        [HttpPost]
        public string UpdateInstancePropertyData([FromQuery] string instanceId, [FromBody] PropertyData propertyValue)
        {
            string retMessage;
            try
            {
                _settingsHubContext.Clients.User(instanceId).SendAsync("UpdateInstanceSettings", propertyValue);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }
    }

}
