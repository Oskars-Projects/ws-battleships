using ConsoleClient.ConsoleHelper;
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
        public OnAskBoatLocationEvent() : base(EventName.AskBoatLocationRequest) { }

        public override void OnMessage(Client client, string? message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("Server did not provide a length for the boat.");
                return;
            }

            Console.WriteLine("Place your next boat with the length of " + message);
            Console.WriteLine("Enter the start location of the boat:");
            Location startLocation = LocationConsoleHelper.AskForLocation();
            Console.WriteLine("Enter the end location of your boat:");
            Location endLocation = LocationConsoleHelper.AskForLocation();

            client.SendMessage(EventName.SendBoatLocationEvent + EventName.SUFFIX + $"[\"{startLocation}\", \"{endLocation}\"]");
        }
    }
}
