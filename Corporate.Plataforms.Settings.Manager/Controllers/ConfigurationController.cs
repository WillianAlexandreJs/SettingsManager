using Corporate.Plataforms.Settings.Manager.Datas.Interfaces;
using Corporate.Plataforms.Settings.Manager.Models;
using Corporate.Plataforms.Settings.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace Corporate.Plataforms.Settings.Manager.Controllers
{
    [ApiController]
    [Route("api/ConfigurationSignalR/[action]")]
    public class ConfigurationController : Controller
    {
        private readonly IHubContext<ConfigurationHubClient> _ConfigurationHubContext;
        private readonly ISettingsData _settingsData;
        private Dictionary<string, Instance> _instances;

        public ConfigurationController(IHubContext<ConfigurationHubClient> ConfigurationHubContext, ISettingsData settingsData)
        {
            _ConfigurationHubContext = ConfigurationHubContext;
            _settingsData = settingsData;
            LoadDictonary();
        }

        private void LoadDictonary()
        {

        }

        [HttpPost]
        public string UpdateApplicationConfiguration([FromBody] ItemConfiguracao itemConfiguracao)
        {
            string retMessage;
            try
            {
                _ConfigurationHubContext.Clients.All.SendAsync("UpdateApplicationConfiguration", itemConfiguracao);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }

        [HttpPost]
        public string UpdateGroupApplicationConfiguration([FromQuery] string applicationName, [FromBody] ItemConfiguracao itemConfiguracao)
        {
            string retMessage;
            try
            {
                _ConfigurationHubContext.Clients.Group(applicationName).SendAsync("UpdateApplicationConfiguration", itemConfiguracao);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }

        [HttpPost]
        public string UpdateClientApplicationConfiguration([FromQuery] string applicationId, [FromBody] ItemConfiguracao itemConfiguracao)
        {
            string retMessage;
            try
            {
                _ConfigurationHubContext.Clients.User(applicationId).SendAsync("UpdateApplicationConfiguration", itemConfiguracao);
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
