using System.Net.WebSockets;
using server.Handlers;
using Server.Game.Chat;

namespace Server.Game.Entities
{
    public class Player
    {
        public string Id { get { return SocketHandler.Connections.GetIdBySocket(Socket); } }
        public WebSocket Socket { get; set; }
        public SocketHandler SocketHandler { get; set; }
        public string? Name { get; set; }


        public PlayerChat Chat { get; private set; }

        public Player(WebSocket socket, SocketHandler socketHandler)
        {
            Socket = socket;
            SocketHandler = socketHandler;

            Chat = new PlayerChat(Socket, SocketHandler);
        }
    }
}
