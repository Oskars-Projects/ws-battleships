using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Events
{
    public abstract class MessageEvent
    {
        public abstract void OnMessage(Client client, string? message);
    }
}
