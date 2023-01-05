using System;
using System.Net.WebSockets;
using Lib.Constants;
using server.Events;
using server.Game.Controllers;
using server.Handlers;
using Server.Game.Entities;

namespace Server.Events
{
    public class OnSendGameNameEvent : GameMessageEvent
	{
		public OnSendGameNameEvent() : base(EventName.SendGameNameEvent) { }

        public override async Task OnGameEvent(SocketHandler handler, GamesController gamesController, Player player, string message)
        {
            // if no game name was provided, we ask for it again
            if(String.IsNullOrWhiteSpace(message))
            {
                player.Chat.SendMessage("You have to provide a valid name!");
                player.Chat.AskForGameName();
                return;
            }
            // trying to find an existing game
            bool success = gamesController.TryGetGameByName(message, out BattleshipsGame? game);

            // create one if it doesnt exist yet
            if (!success)
            {
                game = gamesController.CreateGame(message);
            }

            if (game == null)
            {
                player.Chat.SendMessage("Sorry, we failed to create or find a game!");
                return;
            }

            game.AddPlayer(player);
            player.Chat.SendMessage("Successfully joined game '" + game.Name + "'.");
        }
    }
}

