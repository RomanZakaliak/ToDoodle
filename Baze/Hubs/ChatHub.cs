using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Baze.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message); 
        }
    }
}
