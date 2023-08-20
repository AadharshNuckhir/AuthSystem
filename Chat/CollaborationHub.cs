
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AuthSystem.Chat
{
    public class CollaborationHub : Hub
    {
        private static readonly List<ConnectedClient> ConnectedClients = new List<ConnectedClient>();

        public override async Task OnConnectedAsync()
        {
            var client = new ConnectedClient
            {
                ConnectionId = Context.ConnectionId,
                UserName = "aadharshnuckhir@gmail.com" // Assuming you have authentication enabled
            };

            ConnectedClients.Add(client);
            await base.OnConnectedAsync();
            await SendConnectedClientsList();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var client = ConnectedClients.Find(c => c.ConnectionId == Context.ConnectionId);
            if (client != null)
            {
                ConnectedClients.Remove(client);
                await SendConnectedClientsList();
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendConnectedClientsList()
        {
            await Clients.All.SendAsync("ReceiveConnectedClients", ConnectedClients);
        }

        // Other hub methods...

        private class ConnectedClient
        {
            public string ConnectionId { get; set; }
            public string UserName { get; set; }
        }

        public async Task SendDocumentChange(string content)
        {
            // Broadcast the document change to all connected clients
            await Clients.All.SendAsync("ReceiveDocumentChange", content);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            await SendConnectedClientsList(); // Notify clients about updated connected clients
        }

    }
}
