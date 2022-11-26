using System;
using System.Net.WebSockets;
using System.Text;
using server.Game.Controllers;
using server.SocketManager;

namespace server.Handlers
{
	public abstract class SocketHandler
	{
		public ConnectionManager Connections { get; set; }

		public SocketHandler(ConnectionManager connections)
		{
			Connections = connections;
		}

		public virtual async Task OnConnect(WebSocket socket)
		{
			await Task.Run(() =>
			{
				Connections.AddSocket(socket);
			});
		}

		public virtual async Task OnDisconnect(WebSocket socket)
		{
			await Connections.RemoveSocketAsync(socket);
		}

		public async Task SendMessage(WebSocket socket, string message)
		{
			if (socket.State != WebSocketState.Open)
				return;
			await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message), 0, message.Length), WebSocketMessageType.Text, true, CancellationToken.None);
		}

		public async Task SendMessage(string id, string message)
		{
			await SendMessage(Connections.GetSocketById(id), message);
		}

		public async Task SendBroadcastMessage(string message)
		{
			foreach(KeyValuePair<string, WebSocket> connection in Connections.Connections)
				await SendMessage(connection.Value, message);
		}

		public abstract Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
	}
}

