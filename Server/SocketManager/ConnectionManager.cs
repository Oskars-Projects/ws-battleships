using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace server.SocketManager
{
	public class ConnectionManager
	{
		private ConcurrentDictionary<string, WebSocket> _connections = new ConcurrentDictionary<string, WebSocket>();

		public ConcurrentDictionary<string, WebSocket> Connections { get { return _connections; } }

		public WebSocket GetSocketById(string id)
		{
			return _connections.FirstOrDefault(e => e.Key == id).Value;
		}

		public string GetIdBySocket(WebSocket socket)
		{
			return _connections.FirstOrDefault(e => e.Value == socket).Key;
		}

		public async Task RemoveSocketByIdAsync(string id, string reason = "Socket closed normally!")
		{
			_connections.TryRemove(id, out WebSocket? socket);
			if (socket != null)
				await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, reason, CancellationToken.None);
		}

		public async Task RemoveSocketAsync(WebSocket socket, string reason = "Socket closed normally!")
		{
			await RemoveSocketByIdAsync(GetIdBySocket(socket), reason);
		}

		public void AddSocket(WebSocket socket)
		{
			_connections.TryAdd(Guid.NewGuid().ToString("N"), socket);
		}
	}
}

