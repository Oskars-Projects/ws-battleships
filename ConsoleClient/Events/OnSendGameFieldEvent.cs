using ConsoleClient.ConsoleHelper;
using Lib.Constants;
using Newtonsoft.Json;
using Server.Game.Entities;

namespace ConsoleClient.Events
{
    public class OnSendGameFieldEvent : ServerEvent
    {
        public OnSendGameFieldEvent() : base(EventName.SendGameFieldEvent) { }

        public override void OnMessage(Client client, string? message)
        {
            if (String.IsNullOrWhiteSpace(message)) return;

            GameField[]? gameFields = JsonConvert.DeserializeObject<GameField[]>(message);

            if(gameFields == null)
            {
                Console.WriteLine("Server sent invalid game field.");
                return;
            }
            GameFieldConsoleHelper.PrintGameFields(gameFields);
        }
    }
}
