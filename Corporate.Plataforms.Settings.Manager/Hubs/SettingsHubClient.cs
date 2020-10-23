using AspNetCore.DistributedCache.Tools;
using Corporate.Plataforms.Settings.Manager.Business.Interfaces;
using Corporate.Plataforms.Settings.Manager.Interfaces;
using Corporate.Plataforms.Settings.Manager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corporate.Plataforms.Settings.Manager
{
    [Authorize]
    public class SettingsHubClient : Hub<ISettingsHubClient>
    {
        private readonly ISettingsManagerBusiness _settingsManagerBusiness;

        public SettingsHubClient()
        {
            
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            await base.OnDisconnectedAsync(exception);
        }

       
    }
}

