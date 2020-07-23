using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Todo.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message); 
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", "New user entered chat!");
            await base.OnConnectedAsync();
        }
    }
}
