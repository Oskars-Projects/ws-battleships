using ConsoleClient;
using System.Net.WebSockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        new Client().Start().GetAwaiter().GetResult();
    }
}