using System.Net.WebSockets;
using server.Handlers;
using server.SocketManager;

namespace server.Game.Entities
{
    public class Player
    {
        public string Id { get { return SocketHandler.Connections.GetIdBySocket(Socket); } }
        public WebSocket Socket { get; set; }
        public SocketHandler SocketHandler { get; set; }
        public string? Name { get; set; }

        public Player(WebSocket socket, SocketHandler socketHandler)
        {
            Socket = socket;
            SocketHandler = socketHandler;
        }

        public async Task SendMessage(string message)
        {
            await SocketHandler.SendMessage(Socket, message);
        }
    }
}
