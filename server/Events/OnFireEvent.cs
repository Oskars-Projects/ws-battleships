using lib;
using server.Handlers;
using server.SocketManager;
using System.Net.WebSockets;

namespace server.Events
{
    public class OnFireEvent : MessageEvent
    {
        public OnFireEvent() : base(MessageType.FIRE)
        {
        }

        public override async Task OnEvent(SocketHandler handler, ConnectionManager connections, WebSocket sender, string message)
        {
            await handler.SendBroadcastMessage($"{connections.GetIdBySocket(sender)} shot at {message}");
        }
    }
}
