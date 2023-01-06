using ConsoleClient.ConsoleHelper;
using Lib.Constants;
using Lib.GameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Events
{
    public class OnAskFireLocationEvent : ServerEvent
    {
        public OnAskFireLocationEvent() : base(EventName.AskShootLocationRequst) { }

        public override void OnMessage(Client client, string? message)
        {
            Console.WriteLine("Your turn! Try to shoot the boats of your enemy.");
            Location location = LocationConsoleHelper.AskForLocation();
            client.SendMessage($"{EventName.SendShootLocationEvent}{EventName.SUFFIX}{location}");
        }
    }
}
