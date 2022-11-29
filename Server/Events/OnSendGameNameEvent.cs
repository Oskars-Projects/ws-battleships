using System;
using System.Net.WebSockets;
using Lib.Constants;
using server.Events;
using server.Game.Controllers;
using server.Game.Entities;
using server.Handlers;

namespace Server.Events
{
	public class OnSendGameNameEvent : GameMessageEvent
	{
		public OnSendGameNameEvent() : base(EventName.SendGameNameEvent) { }

        public override async Task OnGameEvent(SocketHandler handler, GamesController gamesController, Player player, string message)
        {
            if(String.IsNullOrWhiteSpace(message))
            {
                await player.SendMessage(EventName.SendMessageEvent + EventName.SUFFIX + "You have to provide a valid name!");
                await player.SendMessage(EventName.AskGameNameRequest + EventName.SUFFIX);
                return;
            }

            bool success = gamesController.TryGetGameByName(message, out BattleshipsGame? game);
            if (!success)
            {
                gamesController.CreateGame(message);
            }
            await player.SendMessage(EventName.AskGameNameRequest + EventName.SUFFIX);
        }
    }
}

