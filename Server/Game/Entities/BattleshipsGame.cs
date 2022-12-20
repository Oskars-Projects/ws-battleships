using Lib.Constants;
using Lib.GameEntities;
using Server.Game.Entities;

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
        
        public Player GetOtherPlayer(Player player)
        {  
            return Player[0] == player ? Player[1] : Player[0];
        }

        public string GetPlayersAsString()
        {
            return Player[0] + " " + Player[1];
        }

        public bool Shoot(Player player, Player otherPlayer, Location location)
        {
            GameField otherPlayersBoard = otherPlayer.GameFields[0];
            if (otherPlayersBoard[location.Y][location.XAsInt] == FieldType.BOAT)
            {
                player.GameFields[1][location.Y][location.XAsInt] = FieldType.HIT;
                return true;
            }
            player.GameFields[1][location.Y][location.XAsInt] = FieldType.NOHIT;
            return false;
        }
    }
}
