using Lib.Constants;
using Lib.GameEntities;
using server.Game.Entities;
using Server.Game.Entities;
using System.Linq;

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

            if (_games.FirstOrDefault(e => e.Name == gameName) == null)
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
            bool exists = TryGetGameByName(gameName, out BattleshipsGame? game);
            if (exists)
            {
                return game!.AddPlayer(player);
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

        public bool TryGetGameByPlayer(string playerName, out BattleshipsGame? game)
        {
            game = _games.FirstOrDefault(game => game.GetPlayersAsString().Contains(playerName));
            return game != null;
        }

        public BattleshipsGame? GetGameByPlayer(Player player)
        {
            return _games.Find(game => game.GetPlayersAsString().Contains(player.Name!));
        }

        public bool CheckIfOpponentHasBoatsLeft(Player player)
        {
            return player.GameFields[1].Board.Any(i => i.Contains(FieldType.BOAT));
        }
        
        public void RemoveGameByPlayer(Player player)
        {
            _games.Remove(GetGameByPlayer(player));
        }
    }
}
