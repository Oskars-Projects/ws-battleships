using System;
using System.Net;
using System.Net.Sockets;

class Server
{

    public static void Main()
    {
        IPAddress serverIpAdress = IPAddress.Any;
        TcpListener server = new TcpListener(serverIpAdress, 5000);
        server.Start();
        Console.WriteLine("Listening on: " + serverIpAdress.ToString());

        server.BeginAcceptTcpClient(HandleConnection, server);

    }

    public static void HandleConnection(IAsyncResult res)
    {
        TcpClient client = 
    }
}