using System;
using System.Net.WebSockets;
using server.Events;
using server.Game.Controllers;
using server.Game.Entities;
using server.Handlers;

namespace Server.Events
{
	public abstract class GameMessageEvent : MessageEvent
	{
        public string Name { get; set; }

		public GameMessageEvent(string eventName)
		{
            Name = eventName;
		}

        public override Task OnEvent(SocketHandler handler, WebSocket sender, string message)
        {
            throw new NotImplementedException();
        }

        public abstract Task OnGameEvent(SocketHandler handler, GamesController gamesController, Player player, string message);
    }
}

