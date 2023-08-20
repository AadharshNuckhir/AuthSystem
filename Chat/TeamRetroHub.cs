using AuthSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.SignalR;

namespace AuthSystem.Chat
{
    public sealed class TeamRetroHub : Hub<ITeamRetroHub>
    {
        static int clientsCount;
        public override async Task OnConnectedAsync()
        {
            clientsCount++;
            await Clients.All.UpdateClientsCount(clientsCount);
            await base.OnConnectedAsync();
        }

        public async Task SendRetroItem(string message)
        {
            await Clients.All.ReceiveRetroItem(message);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clientsCount--;
            await Clients.All.UpdateClientsCount(clientsCount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
