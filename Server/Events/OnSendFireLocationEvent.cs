using Lib.Constants;
using Lib.GameEntities;
using Newtonsoft.Json;
using server.Game.Controllers;
using server.Game.Entities;
using server.Handlers;

namespace Server.Events
{
    public class OnSendFireLocationEvent : GameMessageEvent
    {
        public OnSendFireLocationEvent() : base(EventName.SendFireLocationEvent) { }

        public override async Task OnGameEvent(SocketHandler handler, GamesController gamesController, Player player, string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                await player.SendMessage(EventName.SendMessageEvent + EventName.SUFFIX + "You need to provide a valid location");
                await player.SendMessage(EventName.SendFireLocationEvent + EventName.SUFFIX);
                return;
            }
            string[] tmp = JsonConvert.DeserializeObject<string[]>(message)!;
            Location location = Location.FromString(tmp[0]);
            BattleshipsGame game = gamesController.GetGameByPlayer(player);
            bool IsHit = game!.Shoot(player, gamesController.GetGameByPlayer(player).GetOtherPlayer(player), location);
            if (IsHit)
            {
                await player.SendMessage(EventName.SendMessageEvent + EventName.SUFFIX + "Hit! Shoot one more time");
                await player.SendMessage(EventName.SendBoatLocationEvent + EventName.SUFFIX);
                if (!gamesController.CheckIfOpponentHasBoatsLeft(player))
                {
                    await player.SendMessage(EventName.SendMessageEvent + EventName.SUFFIX + "Game won!");
                    // stop game
                }
            } else
            {
                await player.SendMessage(EventName.SendMessageEvent + EventName.SUFFIX + "No hit, other players turn");
                // next step
            }
        }
    }
}
