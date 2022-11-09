using System;
namespace server
{
    public class Player
    {
        public PlayerSocketBehavior SocketBehaviour { get; private set; }

        public Player(PlayerSocketBehavior socketBehaviour)
        {
            this.SocketBehaviour = socketBehaviour;
        }
    }
}

