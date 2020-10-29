using Corporate.Plataforms.Settings.Manager.Datas.Interfaces;
using Corporate.Plataforms.Settings.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager.Controllers
{
    [ApiController]
    [Route("api/Settings/[action]")]
    public class SettingsHubController : Controller
    {
        private readonly IHubContext<SettingsHubClient> _settingsHubContext;

        public SettingsHubController(IHubContext<SettingsHubClient> settingsHubContext)
        {
            _settingsHubContext = settingsHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGeneralPropertyValue([FromBody] PropertyValue propertyValue)
        {
            await _settingsHubContext.Clients.All.SendAsync("UpdateInstanceSettings", propertyValue);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateApplicationPropertyValue([FromQuery] string applicationName, [FromBody] PropertyValue propertyValue)
        {

            await _settingsHubContext.Clients.Group(applicationName).SendAsync("UpdateInstanceSettings", propertyValue);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInstancePropertyValue([FromQuery] string instanceId, [FromBody] PropertyValue propertyValue)
        {
            await _settingsHubContext.Clients.User(instanceId).SendAsync("UpdateInstanceSettings", propertyValue);
            return Ok();
        }
    }

}
