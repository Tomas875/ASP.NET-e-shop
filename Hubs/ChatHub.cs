using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kursinis.Models;
using Microsoft.AspNetCore.SignalR;

namespace Kursinis.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message) =>
            await Clients.All.SendAsync("reciveMessage", message);
        
            
        
    }
}
