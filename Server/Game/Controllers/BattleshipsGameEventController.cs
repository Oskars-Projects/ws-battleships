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
        }

        /// <summary>
        /// Place a boat on a players field
        /// </summary>1
        /// <param name="boat">Boat that should be placed</param>
        /// <param name="player">Player whos boat should be placed</param>
        public bool PlaceBoat(Boat boat, Player player)
        {
            int currBoatLength = Game.PlayerData[player].BoatLenghtsToPlace.First();

            int boatLength;

            try
            {
                boatLength = boat.Length;
            }
            catch (Exception e)
            {
                player.Chat.SendMessage(e.Message);
                return false;
            }

            // check for valid boat length
            if (boatLength != currBoatLength)
            {
                player.Chat.SendMessage("The boat you were trying to palce should have a length of " + currBoatLength + ", but had actually a length of " + boatLength);
                return false;
            }

            // check if boat is placable and has no sorrounding ships Gamefield
            if (!Game.PlayerData[player].BoatGameField.CheckBoatLocation(boat))
            {
                player.Chat.SendMessage("You can not place a boat next to another boat!");
                return false;
            }

            // place the boat
            bool success = Game.PlayerData[player].BoatGameField.AddBoat(boat);

            // nothing should be able to go wrong any more, but we still check again
            if (!success)
            {
                player.Chat.SendMessage("Invalid location, try again!");
                return false;
            }
            // remove the current boat length from the players list
            Game.PlayerData[player].BoatLenghtsToPlace.Remove(currBoatLength);
            player.Chat.SendMessage("Placing boat successful.");
            return true;
        }

        /// <summary>
        /// Shoots at the other player of the Game
        /// TODO: export the logic of winning to BattleshipsGame class
        /// </summary>
        /// <param name="attacker">The player that shoots</param>
        /// <param name="location">Location where he is shooting at</param>
        /// <returns></returns>
        public bool Shoot(Location location, Player attacker, Player target)
        {
            GameField attackerTargetBoard = Game.PlayerData[attacker].TargetGameField;
            GameField targetBoatBoard = Game.PlayerData[target].BoatGameField;

            // check if it hit
            if (targetBoatBoard.GetTypeFromLocation(location) == FieldType.BOAT)
            {
                attacker.Chat.SendMessage("HIT!");
                target.Chat.SendMessage("You're boat has been hit.");

                // mark boat as destroyed in attacker target field
                attackerTargetBoard[location.YIdx][location.XIdx] = (int)FieldType.HIT;

                // mark boat as gone in target boat field
                targetBoatBoard[location.YIdx][location.XIdx] = (int)FieldType.HIT;
                return true;
            }
                // mark Game field of the attacker since he missed
            attackerTargetBoard[location.YIdx][location.XIdx] = (int)FieldType.NOHIT;
            return false;
        }
    }
}
