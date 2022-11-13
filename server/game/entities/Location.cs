using System;
using System.Reflection;

namespace server.game.entities
{
    public class Location
    {
        public char X { get; private set; }
        public int Y { get; private set; }
        public int XasInt { get { return char.ToUpper(X) - 64; } }

        public Location(char x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
