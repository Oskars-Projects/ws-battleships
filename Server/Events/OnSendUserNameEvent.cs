using System;
using System.Net.WebSockets;
using Lib.Constants;
using server.Events;
using server.Game.Controllers;
using server.Handlers;
using Server.Game.Entities;

namespace Server.Events
{
    public class OnSendUserNameEvent : GameMessageEvent
	{
		public OnSendUserNameEvent() : base(EventName.SendUserNameEvent) { }

        public override async Task OnGameEvent(SocketHandler handler, GamesController gamesController, Player player, string message)
        {
            if(String.IsNullOrWhiteSpace(message))
            {
                player.Chat.SendMessage("You have to provide a valid name!");
                player.Chat.AskForUserName();
                return;
            }

            player.Name = message;
            player.Chat.AskForGameName();
        }
    }
}

