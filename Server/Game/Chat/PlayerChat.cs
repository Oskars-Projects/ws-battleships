using Lib.Constants;
using server.Handlers;
using System.Net.WebSockets;

namespace Server.Game.Chat
{
    public class PlayerChat
    {
        private readonly WebSocket socket;
        private readonly SocketHandler socketHandler;
        internal PlayerChat(WebSocket socket, SocketHandler socketHandler)
        {
            this.socket = socket;
            this.socketHandler = socketHandler;
        }

        private async Task Send(string message)
        {
            await socketHandler.SendMessage(socket, message);
        }

        public async Task SendEventMessage(string eventName, object? data)
        {
            await Send(eventName + EventName.SUFFIX + Convert.ToString(data));
        }

        public async Task SendEventMessage(string eventName)
        {
            await SendEventMessage(eventName, null);
        }

        public async void SendMessage(string message)
        {
            await SendEventMessage(EventName.SendMessageEvent, message);
        }

        public async void AskForBoatLocation(int length)
        {
            await SendEventMessage(EventName.AskBoatLocationRequest, length);
        }

        public async void AskForShootLocation()
        {
            await SendEventMessage(EventName.AskShootLocationRequst);
        }

        public async void AskForGameName()
        {
            await SendEventMessage(EventName.AskGameNameRequest);
        }

        public async void AskForUserName()
        {
            await SendEventMessage(EventName.AskUserNameRequest);
        }
    }
}
