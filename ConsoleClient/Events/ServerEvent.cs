using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Events
{
    public abstract class ServerEvent : MessageEvent
    {
        public string Name { get; private set; }

        public ServerEvent(string name)
        {
            Name = name;
        }
    }
}
