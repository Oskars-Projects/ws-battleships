using ConsoleClient.ConsoleHelper;
using Lib.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Events
{
    public class OnSendGameFieldEvent : ServerEvent
    {
        public OnSendGameFieldEvent() : base(EventName.SendGameFieldEvent) { }

        public override void OnMessage(Client client, string? message)
        {
            if (String.IsNullOrWhiteSpace(message)) return;

            int[][]? gameFields = JsonConvert.DeserializeObject<int[][]>(message);

            if(gameFields == null)
            {
                Console.WriteLine("Server sent invalid game field.");
                return;
            }
            GameFieldConsoleHelper.PrintGameFields(gameFields);
        }
    }
}
