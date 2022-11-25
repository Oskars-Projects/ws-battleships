using Lib.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Events
{
    public class MessageEventHandler : MessageEvent
    {
        private readonly List<ServerEvent> events = new()
        {
            new OnAskForUserNameEvent(),
            new OnAskForGameNameEvent(),
            new OnAskBoatLocationEvent(),
        };

        public override void OnMessage(Client client, string? message)
        {
            if(!String.IsNullOrEmpty(message) && message.Contains(EventName.SUFFIX)) 
            { 
                foreach(ServerEvent serverEvent in events)
                {
                    if(serverEvent.Name == message.Split('$')[0])
                        serverEvent.OnMessage(client, message.Split('$')[1]);
                }
            }
            // TODO: remove
            // for debug, later we will handle events only
            Console.WriteLine(message);
        }
    }
}
