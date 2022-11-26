using Lib.Constants;
using server.Game.Controllers;
using server.Handlers;
using server.SocketManager;
using System.Diagnostics;
using System.Net.WebSockets;

namespace server.Events
{
    public class OnFireEvent : MessageEvent
    {
        public OnFireEvent() : base(EventName.SendFireLocationEvent)
        {
        }

        public override async Task OnEvent(SocketHandler handler, GamesController gamesController, ConnectionManager connections, WebSocket sender, string message)
        {
            // check if even is in game
            // check if is in playing state
            // get game from games controller
            // take actions there
            await handler.SendBroadcastMessage($"{connections.GetIdBySocket(sender)} shot at {message}");
        }
    }
}
