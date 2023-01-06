using Lib.Constants;
using Lib.Extensions;
using Lib.GameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.ConsoleHelper
{
    public static class LocationConsoleHelper
    {
        public static Location AskForLocation()
        {
            return new Location()
            {
                X = AskForX(),
                Y = AskForY(),
            };
        }

        public static char AskForX()
        {
            Console.Write($"X ({GameFieldConstants.XBordersAsString}): ");

            string? message = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("Invalid Input!");
                return AskForX();
            }
            char x = message.ToCharArray()[0];

            if (x.AsInt() < 1 || x.AsInt() > GameFieldConstants.Size)
            {
                Console.WriteLine("X is not in the correct range!");
                return AskForX();
            }
            return x;
        }

        public static int AskForY()
        {
            Console.Write($"Y ({GameFieldConstants.YBordersAsString}): ");

            bool success = int.TryParse(Console.ReadLine(), out int y);

            if (!success)
            {
                Console.WriteLine("Failed to parse y!");
                return AskForY();
            }

            if (y < 1 || y > GameFieldConstants.Size)
            {
                Console.WriteLine("Y is not in the correct range!");
                return AskForY();
            }

            return y;
        }
    }
}
