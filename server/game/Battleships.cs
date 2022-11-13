using System;
namespace server
{
    public class Battleships
    {
        private static Battleships? s_instance;

        private static readonly ushort s_fieldSize = 9;

        private Player[] _players = new Player[2];

        public ushort[,] Field { get; private set; } = new ushort[s_fieldSize, s_fieldSize];



        public static Battleships Instance()
        {
            if (s_instance == null)
                s_instance = new Battleships();

            return s_instance;
        }

        internal void RegisterPlayer(Player player)
        {

            Console.WriteLine("[GAME] Registering new Player");

            if (_players[0] == null)
                _players[0] = player;
            else if (_players[1] == null)
                _players[1] = player;
            else
                Console.WriteLine("[GAME] Registering new Player failed!");
        }

        internal void RegisterHit(Player player, char x, short y)
        {
            // hit or no hit

            // notify where and if hit message

            // update both fields
        }

        internal void RegisterHiddenShip(Player player, Tuple<char, ushort>[] coords)
        {
            // format char to number

            // check if free

            // place it there
        }

        private Player GetOtherPlayer(Player player)
        {
            if (_players[0] == player)
                return _players[1];

            return _players[0];
        }
    }
}

