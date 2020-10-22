using Corporate.Plataforms.Settings.Manager.Business.Interfaces;
using Corporate.Plataforms.Settings.Manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager.Controllers
{
    [ApiController]
    [Route("api/SettingsManager/[action]")]
    public class SettingsManagerController : Controller
    {
        private readonly ISettingsManagerBusiness _settingsManagerBusiness;

        public SettingsManagerController(ISettingsManagerBusiness settingsManagerBusiness)
        {
            _settingsManagerBusiness = settingsManagerBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePropertyValueAsync([FromQuery] string instanceName, [FromBody] PropertyDataUpdate propertyValue)
        {
            await _settingsManagerBusiness.UpdatePropertyValue(instanceName, propertyValue);
            return Ok();
        }

    }

}
