using Lib.Constants;
using server.Events;
using server.Game.Controllers;
using server.SocketManager;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace server.Handlers
{
    public  class EventMessageHandler : SocketMessageHandler
    {
        public List<MessageEvent> Events { get; set; } = new();
        public GamesController GamesController { get; set; }

        public EventMessageHandler(ConnectionManager connection, GamesController gamesController) : base(connection)
        {
            GamesController = gamesController;
        }

        public override async Task ReceiveMessage(WebSocket sender, string message)
        {
            foreach(MessageEvent messageEvent in Events)
            {
                if (!message.StartsWith(messageEvent.Name + EventName.SUFFIX))
                    continue;
                await messageEvent.OnEvent(this, GamesController, Connections, sender, message[(messageEvent.Name.Length + EventName.SUFFIX.Length)..]);
            }
            Debug.WriteLine("received: " + message);
        }
    }
}
