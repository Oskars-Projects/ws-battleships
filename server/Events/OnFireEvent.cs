using lib;
using server.Game.Controllers;
using server.Handlers;
using server.SocketManager;
using System.Diagnostics;
using System.Net.WebSockets;

namespace server.Events
{
    public class OnFireEvent : MessageEvent
    {
        public OnFireEvent() : base(MessageType.FIRE)
        {
        }

        public override async Task OnEvent(SocketHandler handler, GamesController gamesController, ConnectionManager connections, WebSocket sender, string message)
        {
            await handler.SendBroadcastMessage($"{connections.GetIdBySocket(sender)} shot at {message}");
        }
    }
}
