using Microsoft.AspNetCore.SignalR;
using S4U.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S4U.Application.Hubs
{
    public class EquityHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;
            Groups.AddToGroupAsync(Context.ConnectionId, name);

            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string name, List<GetEquityVM> message)
        {
            
            await Clients.Groups(name).SendAsync("ListEquities", message);
        }
    }
}