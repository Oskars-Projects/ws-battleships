using server.Game.Controllers;
using server.Handlers;
using Lib.Constants;
using Newtonsoft.Json;
using Lib.GameEntities;
using Server.Game.Entities;

namespace Server.Events
{
    public class OnSendBoatLocationEvent : GameMessageEvent
    {
        public OnSendBoatLocationEvent() : base(EventName.SendBoatLocationEvent) { }

        public override async Task OnGameEvent(SocketHandler handler, GamesController gamesController, Player player, string message)
        {
            bool exists = gamesController.TryGetGameByPlayer(player, out BattleshipsGame? game);

            if(!exists || game == null)
            {
                player.Chat.SendMessage("You need to join a game before you can place any boats! Try reconnecting!");
                return;
            }

            // in case the player already completed this step, go to next event
            if (game.PlayerData[player].BoatLenghtsToPlace.Count == 0)
            {
                player.Chat.AskForShootLocation();
                return;
            }

            // if message is not provided we ask again
            if (String.IsNullOrWhiteSpace(message))
            {
                player.Chat.SendMessage("You need to provide a valid location");
                return;
            }

            string[] locationArray = JsonConvert.DeserializeObject<string[]>(message);

            if(locationArray == null)
            {
                player.Chat.SendMessage("Location had invalid format!");
                return;
            }

            Location startLocation = Location.FromString(locationArray[0]);
            Location endLocation = Location.FromString(locationArray[1]);

            Boat boat = new(startLocation, endLocation);


            game.PlaceBoat(boat, player);
        }
    }
}
