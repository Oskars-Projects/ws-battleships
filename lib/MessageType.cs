namespace lib;
public class MessageType
{
    public const string SUFFIX = "$";

    // TO SERVER
    public const string FIRE = "fire";
    public const string PLACE_SHIP = "placeship";

    // TO CLIENT
    public const string ENEMY_MOVE = "enemy_move";
    public const string OWN_MOVE_RESULT = "own_move_result";
    public const string GAME_FIELD = "gamefield";
}

