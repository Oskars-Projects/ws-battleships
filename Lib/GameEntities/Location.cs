using Lib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.GameEntities
{
    public class Location
    {

        public int Y { get; set; }
        public char X { get; set; }

        public int XAsInt { get { return X.AsInt(); } }

        public static Location FromString(string str)
        {
            string[] values = str.Trim().Split(';');
            return new Location()
            {
                X = values[0].ToCharArray()[0],
                Y = int.Parse(values[1]),
            };
        }

        public override string? ToString()
        {
            return X + ";" + Y;
        }


        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Location) return false;

            Location loc = (Location)obj;

            return loc.X == X && loc.Y == Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Y, X);
        }
    }
}
