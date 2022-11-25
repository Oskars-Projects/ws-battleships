using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Events
{
    public class OnAskForUserNameEvent : ServerEvent
    {
        public OnAskForUserNameEvent() : base(EventName.AskUserNameRequest) { }

        public override void OnMessage(Client client, string? message)
        {
            Console.Write("Username:\t");
            string? username = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(username))
                OnMessage(client, message);
            else
            {
                client.SendMessage(EventName.SendUserNameEvent + EventName.SUFFIX + username);
            }
        }
    }
}
