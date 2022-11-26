using System;
using System.Net.WebSockets;
using System.Text;
using Lib.Constants;
using Newtonsoft.Json;
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

            // create player object
            // start asking for name
            // will be temporarily added her for test purposes
            // TODO: Remove
            await SendMessage(socket, EventName.AskUserNameRequest + EventName.SUFFIX);
            await SendMessage(socket, EventName.AskBoatLocationRequest+ EventName.SUFFIX + "3");
            await SendMessage(socket, EventName.AskFireLocationRequst + EventName.SUFFIX);
            await SendMessage(socket, EventName.SendGameFieldEvent + EventName.SUFFIX + JsonConvert.SerializeObject(TEMPGenerateGameFields()));
        }


        // TODO: Remove
        private int[][] TEMPGenerateGameFields()
        {
            Random random = new();

            int[][] gameFields = new int[2][];
            for (int i = 0; i < 2; i++)
            {
                int[] values = new int[100];
                for (int j = 0; j < 100; j++)
                {
                    values[j] = random.Next(0, 4);
                }
                gameFields[i] = values;
            }
            return gameFields;
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

