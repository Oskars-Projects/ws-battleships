using System;
using lib;
using WebSocketSharp.NetCore;

class Server
{

    public static void Main()
    {
        using (WebSocket ws = new WebSocket("ws://127.0.0.1:5000/Game"))
        {
            ws.OnMessage += Ws_OnMessage;

            ws.Connect();

            Console.ReadKey();
        }

    }

    private static void Ws_OnMessage(object? sender, MessageEventArgs e)
    {
        Console.WriteLine("Message: " + e.Data);
    }

    private static void Fire(WebSocket ws, string data)
    {
        ws.Send(MessageType.FIRE + MessageType.SUFFIX + data);
    }
}