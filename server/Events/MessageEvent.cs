using server.Handlers;
using server.SocketManager;
using System.Net.WebSockets;


namespace server.Events
{
    public abstract class MessageEvent
    {
        public string EventName { get; private set; }

        public MessageEvent(string eventName) 
        {
            EventName = eventName;
        }

        public abstract Task OnEvent(SocketHandler handler, ConnectionManager connections, WebSocket sender, string message);
    }
}
