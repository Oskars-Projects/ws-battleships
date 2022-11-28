using server.Game.Entities;

namespace server.Game.Controllers
{
    public class GamesController
    {
        private Dictionary<string, BattleshipsGame> _games = new();
        private Queue<Player> _queue = new();
        private List<Player> _players = new();

        public bool CreateGame(string gameName)
        {
            gameName = gameName.Trim();

            if(_games.First(e => e.Key == gameName).Value != null)
            {
                _games.Add(gameName, new BattleshipsGame());
                return true;
            }
            return false;
        }

        public bool CreateGame(string gameName, Player player)
        {
            bool success = CreateGame(gameName);
            if (success)
                _games.GetValueOrDefault(gameName)!.AddPlayer(player);

            return success;
        }

        public bool JoinGame(string gameName, Player player)
        {
            bool exists = _games.TryGetValue(gameName, out BattleshipsGame game);
            if (exists && game != null)
            {
                return game.AddPlayer(player);
            }
            return false;
        }

        public void AddPlayerToQueue(Player player)
        {
            _queue.Enqueue(player);
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public bool TryGetPlayer(string id, out Player? player)
        {
            player = GetPlayerById(id);
            return player != null;
        }

        public Player? GetPlayerById(string id)
        {
            return _players.Find(p => p.Id == id);
        }
    }
}
