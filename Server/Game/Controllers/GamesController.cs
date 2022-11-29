using server.Game.Entities;

namespace server.Game.Controllers
{
    public class GamesController
    {
        private List<BattleshipsGame> _games = new();
        private Queue<Player> _queue = new();
        private List<Player> _players = new();

        public BattleshipsGame? CreateGame(string gameName)
        {
            gameName = gameName.Trim();

            if(_games.First(e => e.Name == gameName) != null)
            {
                BattleshipsGame game = new BattleshipsGame(gameName);
                _games.Add(game);
                return game;
            }
            return null;
        }

        public BattleshipsGame? CreateGame(string gameName, Player player)
        {
            BattleshipsGame? game = CreateGame(gameName);
            if (game != null)
            {
                game.AddPlayer(player);
            }
            return game;
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

        public bool TryGetGameByName(string gameName, out BattleshipsGame? game)
        {
            game = _games.FirstOrDefault(p => p.Name == gameName);
            return game != null;
        }

        public void AddNewGame(string gameName)
        {
            _games.Add(new BattleshipsGame(gameName));
        }
    }
}
