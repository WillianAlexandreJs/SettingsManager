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
    [Route("api/Settings/[action]")]
    public class SettingsHubController : Controller
    {
        private readonly IHubContext<SettingsHubClient> _settingsHubContext;
        private readonly ISettingsData _settingsData;

        public SettingsHubController(IHubContext<SettingsHubClient> settingsHubContext, ISettingsData settingsData)
        {
            _settingsHubContext = settingsHubContext;
            _settingsData = settingsData;
            LoadDictonary();
        }

        private void LoadDictonary()
        {

        }

        [HttpPost]
        public string UpdateGeneralPropertyValue([FromBody] PropertyValue propertyValue)
        {
            string retMessage;
            try
            {
                _settingsHubContext.Clients.All.SendAsync("UpdateGeneralPropertyValue", propertyValue);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }

        [HttpPost]
        public string UpdateApplicationPropertyValue([FromQuery] string applicationName, [FromBody] PropertyValue propertyValue)
        {
            string retMessage;
            try
            {
                _settingsHubContext.Clients.Group(applicationName).SendAsync("UpdateApplicationPropertyValue", propertyValue);
                retMessage = "Success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }

        [HttpPost]
        public string UpdateInstancePropertyValue([FromQuery] string instanceId, [FromBody] PropertyValue propertyValue)
        {
            string retMessage;
            try
            {
                _settingsHubContext.Clients.User(instanceId).SendAsync("UpdateInstancePropertyValue", propertyValue);
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
