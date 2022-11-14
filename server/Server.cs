using lib;
using server;
using WebSocketSharp.NetCore;
using WebSocketSharp.NetCore.Server;

class Server
{
    public static void Main(string[] args)
    {
        WebSocketServer server = new("ws://" + Variables.SERVER_ADDRESS);

        server.AddWebSocketService<PlayerSocketBehavior>("/Game");
        server.Start();
        Console.WriteLine(server.Address);
    }
}

public class PlayerSocketBehavior : WebSocketBehavior
{

    protected override void OnOpen()
    {
        Console.WriteLine("Connection open");
        Battleships.Instance().RegisterPlayer(new Player(this));
        base.OnOpen();
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        if (!e.IsText)
            return;

        string data = e.Data as string;

        string[] values = data.Split(MessageType.SUFFIX);

       

        switch(values[0])
        {
            // fire$1;2
            case MessageType.FIRE:
                OnFire(values[1]);
                break;
            default:
                break;
        }
    }

    private void OnFire(string data)
    {
        Console.WriteLine(data);
    }

}
