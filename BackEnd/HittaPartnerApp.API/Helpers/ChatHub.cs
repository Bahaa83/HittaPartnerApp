using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HittaPartnerApp.API.Helpers
{
    public class ChatHub:Hub
    {
        public async Task  refresh()
        {
            await Clients.All.SendAsync("refresh");
        }
        
    }
}
