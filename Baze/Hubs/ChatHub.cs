using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Todo.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private string _callerName;
        public async Task Send(string message)
        {
            _callerName = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(_callerName))
                await this.Clients.All.SendAsync("Send", $"{_callerName}: " + message);
            else
                await this.Clients.Caller.SendAsync("Internal error, try again!");
        }

        public override async Task OnConnectedAsync()
        {
            _callerName = Context.User.Identity.Name;
            await Clients.All.SendAsync("Notify", $"{_callerName} entered chat!");
            await base.OnConnectedAsync();
        }
    }
}
