using System;
using System.Net.WebSockets;
using System.Text;
using server.SocketManager;

namespace server.Handlers
{
    public class SocketMessageHandler : SocketHandler
    {
        public SocketMessageHandler(ConnectionManager connections) : base(connections)
        {
        }

        public override async Task OnConnect(WebSocket socket)
        {
            await base.OnConnect(socket);
            string socketId = Connections.GetIdBySocket(socket);
            await SendBroadcastMessage($"{socketId} just joined.");
        }

        public override async Task OnDisconnect(WebSocket socket)
        {
            string socketId = Connections.GetIdBySocket(socket);
            await SendBroadcastMessage($"{socketId} left.");
            await base.OnDisconnect(socket);
        }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            string id = Connections.GetIdBySocket(socket);
            string message = $"{id} said: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
            await SendBroadcastMessage(message);
        }
    }
}

