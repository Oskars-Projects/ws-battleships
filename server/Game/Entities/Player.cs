namespace server.Game.Entities
{
    public class Player
    {
        // Is equal to the id of the websocket
        public string Id { get; set; }

        public Player(string id)
        {
            Id = id;
        }
    }
}
