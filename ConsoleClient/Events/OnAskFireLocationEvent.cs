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
        public OnAskFireLocationEvent() : base(EventName.AskFireLocationRequst) { }

        public override void OnMessage(Client client, string? message)
        {
            Location location = LocationConsoleHelper.AskForLocation();
            client.SendMessage($"{EventName.SendFireLocationEvent}{EventName.SUFFIX}{location}");
        }
    }
}
