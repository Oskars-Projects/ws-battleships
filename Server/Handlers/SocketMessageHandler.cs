using System;
using System.Net.WebSockets;
using System.Text;
using Lib.Constants;
using Newtonsoft.Json;
using server.Game.Controllers;
using server.SocketManager;

namespace server.Handlers
{
    public abstract class SocketMessageHandler : SocketHandler
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

        public override async Task Receive(WebSocket sender, WebSocketReceiveResult result, byte[] buffer)
        {
            await ReceiveMessage(sender, Encoding.UTF8.GetString(buffer, 0, result.Count));
        }

        public abstract Task ReceiveMessage(WebSocket sender, string message);

    }
}

