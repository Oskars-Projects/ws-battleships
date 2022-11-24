using System.Net.WebSockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        StartWebsocket().GetAwaiter().GetResult();
    }

    public static async Task StartWebsocket()
    {
        ClientWebSocket socket = new ClientWebSocket();
        await socket.ConnectAsync(new Uri("ws://localhost:5083/ws"), CancellationToken.None);
        Console.WriteLine("Websocket connection established");

        var send = Task.Run(async () => await SendAsync(socket));
        var receive = ReceiveAsync(socket);
        await Task.WhenAll(send, receive);
    }

    public static async Task SendAsync(ClientWebSocket socket)
    {
        string? message;
        while ((message = Console.ReadLine()) != null && !String.IsNullOrEmpty(message))
        {
            if (message == "exit")
                break;

            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }
        Console.WriteLine("Closing Socket");
        await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Connection closed by client.", CancellationToken.None);
    }

    public static async Task ReceiveAsync(ClientWebSocket socket)
    {
        byte[] buffer = new byte[1024 * 4];
        while(true)
        {
            WebSocketReceiveResult wsResult = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            string message = Encoding.UTF8.GetString(buffer, 0, wsResult.Count);
            if(!String.IsNullOrWhiteSpace(message))
                Console.WriteLine("received: " + message);

            if(wsResult.MessageType == WebSocketMessageType.Close)
            {
                if (socket.State != WebSocketState.Closed)
                    await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Connection closed by host.", CancellationToken.None);
                break;
            }
        }
    }
}