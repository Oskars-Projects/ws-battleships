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
        public const int s_gamePlayerSize = 2;
        public string Name { get; set; }
        public bool Over { get; set; } = false;
        public List<Player> Players { get; private set; } = new List<Player>();

        public Dictionary<Player, PlayerData> PlayerData = new Dictionary<Player, PlayerData>();

        public BattleshipsGameChat Chat { get; private set; }
        public BattleshipsGameEventController Controller { get; private set; }

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
                p.Chat.SendGameField(data.BoatGameField);
                AskPlayerForBoatLocation(p);
            }
        }


        public void AskPlayerForBoatLocation(Player player)
        {
            PlayerData data = PlayerData[player];

            // return if the player does not have boats to place
            if (data.BoatLenghtsToPlace.Count == 0) return;

            player.Chat.AskForBoatLocation(data.BoatLenghtsToPlace.First());
        }

        public void PlaceBoat(Boat boat, Player player)
        {
            Controller.PlaceBoat(boat, player);
            player.Chat.SendGameField(PlayerData[player].BoatGameField);

            if (PlayerData[player].BoatLenghtsToPlace.Count != 0)
            {
                AskPlayerForBoatLocation(player);
            }
            else
            {
                Player enemy = GetOtherPlayer(player);
                PlayerData enemyPlayerData = PlayerData[enemy];
                if (enemyPlayerData.BoatLenghtsToPlace.Count == 0)
                {
                    // ask first player for shoot location, after both players are finished placing
                    AskPlayerForShootLocation(player);
                }
                else
                {
                    player.Chat.SendMessage($"Waiting for {enemy.Name} to finish.");
                }
            }
        }

        public void AskPlayerForShootLocation(Player p)
        {
            GetOtherPlayer(p).Chat.SendMessage($"It is {p.Name}'s turn.");

            PlayerData data = PlayerData[p];
            p.Chat.SendGameFields(new GameField[]{ data.BoatGameField, data.TargetGameField });
            p.Chat.AskForShootLocation();
        }

        public void Shoot(Location location, Player attacker)
        {
            Player target = GetOtherPlayer(attacker);
            bool hit = Controller.Shoot(location, attacker, target);

            // show both players the new game field after a shot
            PlayerData attackerData = PlayerData[attacker];
            attacker.Chat.SendGameFields(new GameField[] { attackerData.BoatGameField, attackerData.TargetGameField });
            PlayerData targetData = PlayerData[target];
            attacker.Chat.SendGameFields(new GameField[] { targetData.BoatGameField, targetData.TargetGameField });

            if (hit)
            {
                // check if the targets field still contains a baot
                if (!PlayerData[target].BoatGameField.ContainsBoat())
                {
                    // end the Game and set the attacker as winner
                    EndGame(attacker, target);
                    return;
                }
                // if hit then its attacker's turn again
                AskPlayerForShootLocation(attacker);
            }
            else
            {
                // enemys turn if nothing got hit
                AskPlayerForShootLocation(target);
            }
        }

        public bool AddPlayer(Player player)
        {
            try
            {
                Players.Add(player);
                Chat.SendJoinMessage(player);

                // initialize player values
                PlayerData.Add(player, new PlayerData());

                if (Players.Count == s_gamePlayerSize)
                    Start();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the other player
        /// TODO: find better way to do this
        /// </summary>
        /// <param name="player">a player</param>
        /// <returns>the other player</returns>
        public Player GetOtherPlayer(Player player)
        {
            return Players[0] == player ? Players[1] : Players[0];
        }

        public void EndGame(Player winner, Player loser)
        {
            Over = true;

            winner.Chat.SendMessage("You won!");
            loser.Chat.SendMessage("You lost!");
            Chat.SendGameOverMessage(winner);
        }
    }

    public class PlayerData
    {
        public GameField BoatGameField { get; private set; } = new GameField();
        public GameField TargetGameField { get; private set; } = new GameField();
        public List<int> BoatLenghtsToPlace { get; private set; } = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2 };
    }
}
