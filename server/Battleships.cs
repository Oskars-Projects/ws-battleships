using System;
namespace server
{
    public class Battleships
    {
        private static Battleships INSTANCE;

        private Player[] players = new Player[2];

        public static Battleships Instance()
        {
            if (INSTANCE == null)
                INSTANCE = new Battleships();

            return INSTANCE;
        }

        internal void RegisterPlayer(Player player)
        {

            Console.WriteLine("[GAME] Registering new Player");

            if (players[0] != null)
                players[0] = player;
            else if (players[1] != null)
                players[1] = player;
            else
                Console.WriteLine("[GAME] Registering new Player failed!");
        }

        public Battleships()
        {

        }
    }
}

