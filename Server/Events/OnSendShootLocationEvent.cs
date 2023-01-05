using Lib.Constants;
using Lib.GameEntities;
using Newtonsoft.Json;
using server.Game.Controllers;
using server.Handlers;
using Server.Game.Entities;

namespace Server.Events
{
    public class OnSendShootLocationEvent : GameMessageEvent
    {
        public OnSendShootLocationEvent() : base(EventName.SendShootLocationEvent) { }

        public override async Task OnGameEvent(SocketHandler handler, GamesController gamesController, Player player, string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                player.Chat.SendMessage("You need to provide a valid location");
                player.Chat.AskForShootLocation();
                return;
            }

            string[] tmp = JsonConvert.DeserializeObject<string[]>(message)!;
            Location location = Location.FromString(tmp[0]);
            
            bool exists = gamesController.TryGetGameByPlayer(player, out BattleshipsGame? game);

            if(!exists || game == null)
            {
                player.Chat.SendMessage("You need to join a game before you can shoot! Try reconnecting!");
                return;
            }

            game.Shoot(location, player);
        }
    }
}
