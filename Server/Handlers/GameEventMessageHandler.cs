using Lib.Constants;
using server.Events;
using server.Game.Controllers;
using server.SocketManager;
using Server.Events;
using Server.Game.Entities;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace server.Handlers
{
    public  class GameEventMessageHandler : SocketMessageHandler
    {
        public List<MessageEvent> Events { get; set; } = new();
        public GamesController GamesController { get; set; }

        public GameEventMessageHandler(ConnectionManager connection, GamesController gamesController) : base(connection)
        {
            GamesController = gamesController;
        }

        public override async Task OnConnect(WebSocket socket)
        {
            await base.OnConnect(socket);
            Player player = new Player(socket, this);

            GamesController.AddPlayer(player);
            player.Chat.AskForUserName();
        }

        public override async Task ReceiveMessage(WebSocket sender, string message)
        {
            bool success = GamesController.TryGetPlayer(Connections.GetIdBySocket(sender), out Player? player);

            if (!success)
            {
                player = new Player(sender, this);
                GamesController.AddPlayer(player);
            }

            foreach(GameMessageEvent gameMessageEvent in Events)
            {
                if (!message.StartsWith(gameMessageEvent.Name + EventName.SUFFIX))
                    continue;
                await gameMessageEvent.OnGameEvent(this, GamesController, player, message[(gameMessageEvent.Name.Length + EventName.SUFFIX.Length)..]);
            }
            Debug.WriteLine("received: " + message);
        }
    }
}
