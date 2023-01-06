using Lib.Constants;
using Lib.Extensions;
using Lib.GameEntities;
using Newtonsoft.Json;

namespace Server.Game.Entities
{
    public class GameField
    {
        public int[][] Board = new int[GameFieldConstants.Size][];

        public GameField()
        {
            InitializeArray();
        }

        public int[] this[int y]
        {
            get { return Board[y]; }
            set { Board[y] = value; }
        }

        /// <summary>
        /// Adds a boat to the current game field
        /// </summary>
        /// <param name="boat">the boat to add</param>
        /// <returns>returns true if the boat was placed successfully and false if not</returns>
        public bool AddBoat(Boat boat)
        {
            int startX = Math.Min(boat.Start.XIdx, boat.End.XIdx);
            int endX = Math.Max(boat.Start.XIdx, boat.End.XIdx);

            int startY = Math.Min(boat.Start.YIdx, boat.End.YIdx);
            int endY = Math.Max(boat.Start.YIdx, boat.End.YIdx);

            for (int x = startX; x <= endX; x++)
            {
                for(int y = startY; y <= endY; y++)
                {
                    if ((FieldType) this[y][x] != FieldType.WATER) return false;
                    this[y][x] = (int) FieldType.BOAT;
                }
            }
            return true;
        }

        public FieldType GetTypeFromLocation(Location location)
        {
            return (FieldType) this[location.YIdx][location.XIdx];
        }

        /// <summary>
        /// Checks that the boat's and the boats surrounding locations are water
        /// </summary>
        /// <param name="boat">Boat</param>
        /// <returns>if all locations are water</returns>
        public bool CheckBoatLocation(Boat boat)
        {
            if (boat.Direction == Direction.HORIZONTAL)
            {
                int startX = Math.Min(boat.Start.XIdx, boat.End.XIdx);
                int endX = Math.Max(boat.Start.XIdx, boat.End.XIdx);

                // try to remove 1 from the start if possible
                if (startX > 0) startX--;
                // try to add 1 to the end if possible
                if (endX < GameFieldConstants.Size - 1) endX++;

                int y = boat.Start.YIdx;

                for (int x = startX; x < endX; x++)
                {
                    // check one above if possible
                    if (y > 0)
                        if ((FieldType) this[y - 1][x] != FieldType.WATER) 
                            return false;

                    // check current y
                    if ((FieldType) this[y][x] != FieldType.WATER)
                        return false;

                    // check one below if possible
                    if (y < GameFieldConstants.Size - 1)
                        if ((FieldType) this[y + 1][x] != FieldType.WATER)
                            return false;
                }
                return true;
            }

            if (boat.Direction == Direction.VERTICAL)
            {
                int startY = Math.Min(boat.Start.YIdx, boat.End.YIdx);
                int endY = Math.Max(boat.Start.YIdx, boat.End.YIdx);

                // try to remove 1 from the start if possible
                if (startY > 0) startY--;
                // try to add 1 to the end if possible
                if (endY < GameFieldConstants.Size - 1) endY++;

                int x = boat.Start.XIdx;

                for (int y = startY; y < endY; y++)
                {
                    // check one left if possible
                    if (x > 0)
                        if ((FieldType) this[y][x - 1] != FieldType.WATER)
                            return false;

                    // check current x
                    if ((FieldType) this[y][x] != FieldType.WATER)
                        return false;

                    // check one right if possible
                    if (x < GameFieldConstants.Size - 1)
                        if ((FieldType) this[y][x + 1] != FieldType.WATER)
                            return false;
                }
                return true;
            }
            return false;
        }

        public bool ContainsBoat()
        {
            for (int y = 0; y < Board.Length; y++)
            {
                for (int x = 0; x < this[y].Length; x++)
                {
                    if ((FieldType)this[y][x] == FieldType.BOAT)
                        return true;
                }
            }
            return false;
        }

        private void InitializeArray()
        {
            for (int i = 0; i < Board.Length; i++) 
            {
                int[] temp = new int[GameFieldConstants.Size];
                for (int j = 0; j < temp.Length; ++j)
                {
                    temp[j] = 0;
                }
                Board[i] = temp;
            }
        }

        public int BoatPartCount()
        {
            int count = 0;

            foreach (int[] row in Board)
            {
                foreach(int cell in row)
                {
                    if (cell == (int)FieldType.BOAT) count++;
                }
            }

            return count;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(Board);
        }
    }
}
