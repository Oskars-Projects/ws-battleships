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

            int currBoatLength = game.PlayerData[player].BoatLenghtsToPlace.First();

            // if message is not provided we ask again
            if (String.IsNullOrWhiteSpace(message))
            {
                player.Chat.SendMessage("You need to provide a valid location");
                player.Chat.AskForBoatLocation(currBoatLength);
                return;
            }

            string[] locationArray = JsonConvert.DeserializeObject<string[]>(message)!;
            Location startLocation = Location.FromString(locationArray[0]);
            Location endLocation = Location.FromString(locationArray[1]);

            Boat boat = new(startLocation, endLocation);

            int boatLength;

            try
            {
                boatLength = boat.Length;
            }
            catch(Exception e)
            {
                player.Chat.SendMessage(e.Message);
                player.Chat.AskForBoatLocation(currBoatLength);
                return;
            }

            // check for valid boat length
            if (boatLength != currBoatLength)
            {
                player.Chat.SendMessage("The boat you were trying to palce should have a length of " + currBoatLength + ", but had actually a length of " + boatLength);
                player.Chat.AskForBoatLocation(currBoatLength);
                return;
            }

            // check if boat is placable and has no sorrounding ships gamefield
            if (!game.PlayerData[player].BoatGameField.CheckBoatLocation(boat))
            {
                player.Chat.SendMessage("You can not place a boat next to another boat!");
                player.Chat.AskForBoatLocation(currBoatLength);
                return;
            }

            // place the boat
            bool success = game.PlayerData[player].BoatGameField.AddBoat(boat);

            // nothing should be able to go wrong any more, but we still check again
            if (!success)
            {
                player.Chat.SendMessage("Invalid location, try again!");
                player.Chat.AskForBoatLocation(currBoatLength);
                return;
            }

            // remove the current boat length from the players list
            game.PlayerData[player].BoatLenghtsToPlace.Remove(currBoatLength);
            player.Chat.SendMessage("Placing boat successful.");


            // if there are still boats to place, ask for the next boat
            if (game.PlayerData[player].BoatLenghtsToPlace.Count > 0)
            {
                player.Chat.SendMessage("Place boat with size of " + currBoatLength);
                player.Chat.AskForBoatLocation(currBoatLength);
            }
            else
            {
                player.Chat.AskForShootLocation();
            }
        }
    }
}
