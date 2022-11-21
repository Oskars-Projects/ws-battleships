﻿using System;
using lib;
using WebSocketSharp.NetCore;

class Server
{

    public static void Main()
    {
        using (WebSocket ws = new WebSocket("ws://" + Variables.SERVER_ADDRESS + "/Game"))
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

    private static void SendEventMessage(WebSocket ws, MessageType msgType, string data)
    {
        ws.Send(msgType + MessageType.SUFFIX + data);
    }
}