using lib;
using server.Events;
using server.SocketManager;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace server.Handlers
{
    public  class EventMessageHandler : SocketMessageHandler
    {
        public List<MessageEvent> Events { get; set; } = new();

        public EventMessageHandler(ConnectionManager connection) : base(connection)
        {
        }

        public override async Task ReceiveMessage(WebSocket sender, string message)
        {
            foreach(MessageEvent messageEvent in Events)
            {
                if (!message.StartsWith(messageEvent.EventName + MessageType.SUFFIX))
                    continue;
                await messageEvent.OnEvent(this, Connections, sender, message[(messageEvent.EventName.Length + MessageType.SUFFIX.Length)..]);
            }
        }
    }
}
