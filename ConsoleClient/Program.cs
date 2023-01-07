using ConsoleClient;
using System.Net.WebSockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        new Client("wss://api.battleships.oskars.tech/game").Start().GetAwaiter().GetResult();
    }
}