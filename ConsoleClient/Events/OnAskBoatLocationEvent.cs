using Lib.Constants;
using Lib.Extensions;
using Lib.GameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Events
{
    public class OnAskBoatLocationEvent : ServerEvent
    {
        public OnAskBoatLocationEvent() : base(EventName.AskBoatLocationRequest)
        {
        }

        public override void OnMessage(Client client, string? message)
        {
            if (message == null)
                throw new InvalidDataException("Server did not provide length of the boat");

            Console.WriteLine("Place your next boat with the length of " + message);
            Console.WriteLine("Enter the start location of the boat:");
            Location startLocation = AskForLocation();
            Console.WriteLine("Enter the end location of your boat:");
            Location endLocation = AskForLocation();

            client.SendMessage(EventName.SendBoatLocationEvent + EventName.SUFFIX + $"['{startLocation}', '{endLocation}']");
        }

        private Location AskForLocation()
        {
            return new Location()
            {
                X = AskForX(),
                Y = AskForY(),
            };
        }

        private char AskForX()
        {
            Console.Write($"X ({GameFieldConstants.XBordersAsString}): ");

            string message = Console.ReadLine();

            if (message == null)
            {
                Console.WriteLine("Invalid Input!");
                return AskForX();
            }
            char x = message.ToCharArray()[0];

            if(x.AsInt() < 1 || x.AsInt() > GameFieldConstants.Size)
            {
                Console.WriteLine("X is too high or low!");
                return AskForX();
            }

            return x;
        }

        private int AskForY()
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
                Console.WriteLine("Y is too high or low!");
                return AskForX();
            }

            return y;
        }
    }
}
