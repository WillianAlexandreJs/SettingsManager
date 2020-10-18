using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Corporate.Plataforms.Settings.Manager
{
    public class ApplicationIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext context)
        {
            return context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}