using server.Game.Controllers;
using server.Game.Entities;
using server.Handlers;
using Lib.Constants;
using Newtonsoft.Json;
using Lib.GameEntities;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.Runtime.ExceptionServices;

namespace Server.Events
{
    public class OnSendBoatLocationEvent : GameMessageEvent
    {
        public OnSendBoatLocationEvent() : base(EventName.SendBoatLocationEvent) { }

        public override async Task OnGameEvent(SocketHandler handler, GamesController gamesController, Player player, string message)
        {
            if (!player.HasBoatsToPlace())
            {
                // next step
            }

            // check if provided boat is correct length
            // check if placable
            // place
            if (String.IsNullOrWhiteSpace(message))
            {
                await player.SendMessage(EventName.SendMessageEvent + EventName.SUFFIX + "You need to provide a valid location");
                await player.SendMessage(EventName.SendBoatLocationEvent + EventName.SUFFIX);
                return;
            }
            string[] tmp = JsonConvert.DeserializeObject<string[]>(message)!;
            Location startLocation = Location.FromString(tmp[0]);
            Location endLocation = Location.FromString(tmp[1]);
            bool success = player.GameFields[0].SetBoatLocation(startLocation, endLocation, player.BoatsToPlace.First());
            if (success)
            {
                player.BoatsToPlace.Remove(player.BoatsToPlace.First());
                await player.SendMessage(EventName.SendMessageEvent + EventName.SUFFIX + "Placing boat successful. Boats to place: " + player.BoatsToPlace.Count);
            }

            if (player.HasBoatsToPlace())
            {
                await player.SendMessage(EventName.SendMessageEvent + EventName.SUFFIX + "Place boat with size of " + player.BoatsToPlace.First());
                await player.SendMessage(EventName.SendBoatLocationEvent + EventName.SUFFIX);
            }
            else
            {
                //next step
            }
        }
    }
}
