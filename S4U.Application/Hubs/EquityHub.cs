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
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "HubUsers");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "HubUsers");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(List<GetEquityVM> message)
        {
            await Clients.Groups("HubUser").SendAsync("ListEquities", message);
        }
    }
}