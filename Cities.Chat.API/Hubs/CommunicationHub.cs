using Cities.Chat.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace Cities.Chat.API.Hubs
{
    public class CommunicationHub : Hub
    {
        private static List<Player> ConnectedUsers = new List<Player>();

        public Task GetCities()
        {
            var cities = new List<City>();

            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                cities = applicationContext.Cities.ToList();
            }

            return Clients.Caller.SendAsync("ReceiveCities", cities);
        }
        public async Task SendMessageToUser(string targetUserId, City city)
        {
            await Clients.Client(targetUserId).SendAsync("ReceiveMessageFromUser", city);
        }

        public async Task SendCloseLobby(string targetUserId)
        {
            await Clients.Client(targetUserId).SendAsync("CloseLobby");
        }

        public async Task SendAnswerToPlay(string targetUserId, string sendersUserId, string name)
        {
            await Clients.Client(targetUserId).SendAsync("OpenGameLobby", sendersUserId, name);
        }

        public async Task SendRequestToPlay(string targetUserId, string userName, string requestUserId)
        {
            await Clients.Client(targetUserId).SendAsync("RequestToPlay", targetUserId, userName, requestUserId);
        }


        public Task SendMessageToAll(string message, string name)
        {
            return Clients.Others.SendAsync("ReceiveMessage", message, name);
        }

        public Task SendConnectedUsers()
        {
            return Clients.Caller.SendAsync("GetConnectedUsers", ConnectedUsers);
        }

        public void AddUser(string playerName)
        {
            ConnectedUsers.Add(new Player() { ConnectionId = Context.ConnectionId, PlayerName = playerName });
        }

        public void RemoveUser()
        {
            ConnectedUsers.Remove(ConnectedUsers.First(x => Context.ConnectionId == x.ConnectionId));
        }

        public void ClearUsers()
        {
            ConnectedUsers.Clear();
        }

        public override Task OnConnectedAsync()
        {
            //ConnectedUsers.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (ConnectedUsers.Count > 0)
            {
                ConnectedUsers.Remove(ConnectedUsers.First(x => Context.ConnectionId == x.ConnectionId));
            }
            
            return base.OnDisconnectedAsync(exception);
        }
    }
}
