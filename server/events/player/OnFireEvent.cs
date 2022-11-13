using System;
using server.game.entities;

namespace server.events.player
{
    public class OnPlayerFireEventArgs : EventArgs
    {
        public Player Sender { get; private set; }
        public Player Receiver { get; private set; }
        public Location Location { get; private set; }

        public OnPlayerFireEventArgs(Player sender, Player receiver, Location location)
        {
            Sender = sender;
            Receiver = receiver;
            Location = location;
        }
    }
}

