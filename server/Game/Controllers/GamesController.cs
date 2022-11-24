using server.Game.Entities;

namespace server.Game.Controllers
{
    public class GamesController
    {
        public Dictionary<string, BattleshipsGame> _games = new();

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
                _games.GetValueOrDefault(gameName).AddPlayer(player);

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

    }
}
