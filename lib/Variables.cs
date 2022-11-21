using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib
{
    public static class Variables
    {
        
        public static string SERVER_IP = Environment.GetEnvironmentVariable("SERVER_IP") ?? "172.0.0.1";
        public static string SERVER_PORT = Environment.GetEnvironmentVariable("SERVER_PORT") ?? "5000";
        public static string SERVER_ADDRESS = SERVER_IP + ":" + SERVER_PORT; 

    }
}
