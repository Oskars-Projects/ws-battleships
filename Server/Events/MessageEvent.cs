using server.Game.Controllers;
using server.Handlers;
using server.SocketManager;
using System.Net.WebSockets;


namespace server.Events
{
    public abstract class MessageEvent
    { 
        public abstract Task OnEvent(SocketHandler handler, WebSocket sender, string message);
    }
}
