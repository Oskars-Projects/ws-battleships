using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Constants
{
    public static class EventName
    {
        public const string SUFFIX = "$";

        // PLAYER EVENTS
        public const string SendUserNameEvent = "p_sendUserName";
        public const string SendGameNameEvent = "p_sendGameName";
        public const string SendBoatLocationEvent = "p_sendBoatLocation";
        public const string SendShootLocationEvent = "p_sendShootLocation";

        // SERVER EVENTS
        public const string AskUserNameRequest = "s_askUserName";
        public const string AskGameNameRequest = "s_askGameName";
        public const string AskBoatLocationRequest = "s_askBoatLocation";
        public const string AskShootLocationRequst = "s_askShootLocation";
        public const string SendMessageEvent = "s_sendMessage";
        public const string SendGameFieldEvent = "s_sendGameField";

    }
}
