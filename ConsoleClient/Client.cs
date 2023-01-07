using ConsoleClient.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
	public class Client
	{
		private readonly ClientWebSocket socket = new();
		private readonly Queue<string> sendMessageQueue = new();
		public string Url { get; set; }

		public Client(string url)
		{
			Url = url;
		}

		public void SendMessage(string message)
		{
			sendMessageQueue.Enqueue(message);
		}

		public bool IsConnected()
		{
			return socket.State == WebSocketState.Open;
		}

		public async Task Start()
		{
			await socket.ConnectAsync(new Uri("wss://api.battleships.oskars.tech/game"), CancellationToken.None);
			Console.WriteLine("Websocket connection established");

			Task send = Task.Run(async () => await SendAsync());
			Task receive = ReceiveAsync();
			await Task.WhenAll(send, receive);
		}

		private async Task SendAsync()
		{
			while (true)
			{
				if (!(sendMessageQueue.Count > 0))
					continue;
				string message = sendMessageQueue.Dequeue();

				if (message == "exit")
					break;

				byte[] bytes = Encoding.UTF8.GetBytes(message);
				await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
			}
			Console.WriteLine("Closing Socket");
			await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Connection closed by client.", CancellationToken.None);
		}

		private async Task ReceiveAsync()
		{
			byte[] buffer = new byte[1024 * 4];
			while (true)
			{
				WebSocketReceiveResult wsResult = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

				string message = Encoding.UTF8.GetString(buffer, 0, wsResult.Count);
				if (!String.IsNullOrWhiteSpace(message))
					new MessageEventHandler().OnMessage(this, message);

				if (wsResult.MessageType == WebSocketMessageType.Close)
				{
					if (socket.State != WebSocketState.Closed)
						await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Connection closed by host.", CancellationToken.None);
					break;
				}
			}
		}
	}
}
