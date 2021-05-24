 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using NomadDashboardsAPI.Models;

namespace NomadDashboardsAPI.HubConfig
{
    public class ChatHub : Hub
    {

        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("revieveMessage", message);
        }

        public Task SendMessage1(string user, string message)               // Two parameters accepted
        {
            return Clients.All.SendAsync("ReceiveOne", user, message);    // Note this 'ReceiveOne' 
        }
    }
}
