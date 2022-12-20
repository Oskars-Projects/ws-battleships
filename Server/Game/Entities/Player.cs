using System.Net.WebSockets;
using server.Handlers;
using server.SocketManager;
using Server.Game.Entities;

namespace server.Game.Entities
{
    public class Player
    {
        public string Id { get { return SocketHandler.Connections.GetIdBySocket(Socket); } }
        public WebSocket Socket { get; set; }
        public SocketHandler SocketHandler { get; set; }
        public string? Name { get; set; }
        public GameField[] GameFields = new GameField[2] {new GameField(), new GameField()};
        public List<int> BoatsToPlace = new List<int> {5, 4, 4, 3, 3, 3, 2, 2};

        public Player() { }
        public Player(WebSocket socket, SocketHandler socketHandler)
        {
            Socket = socket;
            SocketHandler = socketHandler;
        }

        public async Task SendMessage(string message)
        {
            await SocketHandler.SendMessage(Socket, message);
        }

        public bool HasBoatsToPlace()
        {
            return BoatsToPlace.Any();
        }
    }
}
