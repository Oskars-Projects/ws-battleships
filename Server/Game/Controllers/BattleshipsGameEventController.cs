using Lib.Constants;
using Lib.GameEntities;
using Server.Game.Entities;

namespace Server.Game.Controllers
{
    public class BattleshipsGameEventController
    {

        public BattleshipsGame Game { get; private set; }

        public BattleshipsGameEventController(BattleshipsGame game)
        {
            Game = game;
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            Game.OnShoot += new BattleshipsGame.OnShootEventHandler(Shoot);
        }

        /// <summary>
        /// Shoots at the other player of the game
        /// </summary>
        /// <param name="attacker">The player that shoots</param>
        /// <param name="location">Location where he is shooting at</param>
        /// <returns></returns>
        public void Shoot(Location location, Player attacker, Player target)
        {
            GameField otherPlayerBoatBoard = Game.PlayerData[target].BoatGameField;

            // check if it hit
            if (otherPlayerBoatBoard.GetTypeFromLocation(location) == FieldType.BOAT)
            {
                // mark boat as destroyed in attacker target field
                Game.PlayerData[attacker].TargetGameField[location.Y][location.XAsInt] = (int)FieldType.HIT;

                // mark boat as gone in target boat field
                otherPlayerBoatBoard[location.Y][location.XAsInt] = (int)FieldType.HIT;

                // check if the targets field still contains a baot
                if(!otherPlayerBoatBoard.ContainsBoat())
                {
                    // end the game and set the attacker as winner
                    EndGame(attacker, target);
                    return;
                }
            } 
            else
            {
                // mark game field of the attacker since he missed
                Game.PlayerData[attacker].TargetGameField[location.Y][location.XAsInt] = (int)FieldType.NOHIT;
            }

            target.Chat.AskForShootLocation();
            return;
        }

        public void EndGame(Player winner, Player loser)
        {
            Game.Over = true;

            winner.Chat.SendMessage("You won!");
            loser.Chat.SendMessage("You lost!");
            Game.Chat.SendGameOverMessage(winner);
        }

    }
}
