namespace server.Game.Entities
{
    public class BattleshipsGame
    {
        private const int s_gamePlayerSize = 2;
        public string Name { get; set; }
        public Player[] Player { get; private set; } = new Player[s_gamePlayerSize];
        
        public BattleshipsGame(string name)
        {
            Name = name;
        }

        public BattleshipsGame(Player player, string name)
        {
            AddPlayer(player);
            Name = name;
        }

        public bool AddPlayer(Player player)
        {
            if (Player[0] == null)
                Player[0] = player;
            else if (Player[1] == null)
                Player[1] = player;
            else
                return false;
            return true;
        }
    }
}
