using Lib.Constants;
using Lib.GameEntities;
using Server.Events;
using Server.Game.Chat;
using Server.Game.Controllers;
using System.Numerics;

namespace Server.Game.Entities
{


    public class BattleshipsGame
    {
        public delegate void OnShootEventHandler(Location location, Player attacker, Player target);
        
        
        
        public const int s_gamePlayerSize = 2;
        public string Name { get; set; }
        public bool Over { get; set; } = false;
        public List<Player> Players { get; private set; } = new List<Player>();

        public Dictionary<Player, PlayerData> PlayerData = new Dictionary<Player, PlayerData>();

        public BattleshipsGameChat Chat { get; private set; }
        public BattleshipsGameEventController Controller { get; private set; }

        // events
        public event OnShootEventHandler? OnShoot;

        public BattleshipsGame(string name)
        {
            Name = name;
            Chat = new BattleshipsGameChat(this);
            Controller = new BattleshipsGameEventController(this);

            Players.Capacity = s_gamePlayerSize;
        }

        public BattleshipsGame(Player player, string name) : this(name)
        {
            AddPlayer(player);
        }
        public void Start()
        {
            Chat.SendStartMessage();

            foreach (Player p in Players)
            {
                bool success = PlayerData.TryGetValue(p, out PlayerData? data);
                if (!success || data == null) continue;
                p.Chat.AskForBoatLocation(data.BoatLenghtsToPlace[0]);
            }
        }

        public void Shoot(Location location, Player attacker)
        {
            Player target = GetOtherPlayer(attacker);
            this.OnShoot?.Invoke(location, attacker, target);
        }

        public bool AddPlayer(Player player)
        {
            try
            {
                this.Players.Add(player);
                Chat.SendJoinMessage(player);

                // initialize player values
                PlayerData.Add(player, new PlayerData());

                if(Players.Count == s_gamePlayerSize)
                    Start();
                

                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public Player GetOtherPlayer(Player player)
        {
            return Players[0] == player ? Players[0] : Players[1];
        }    
    }



    public class PlayerData
    {
        public GameField BoatGameField { get; private set; } = new GameField();
        public GameField TargetGameField { get; private set; } = new GameField();
        public List<int> BoatLenghtsToPlace { get; private set; } = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2 };
    }
}
