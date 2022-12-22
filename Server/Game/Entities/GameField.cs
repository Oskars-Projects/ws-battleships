using Lib.Constants;
using Lib.Extensions;
using Lib.GameEntities;

namespace Server.Game.Entities
{
    public class GameField
    {
        public int[][] Board = new int[GameFieldConstants.Size][];

        public GameField()
        {
            InitializeArray();
        }

        public int[] this[int x]
        {
            get { return Board[x]; }
            set { Board[x] = value; }
        }

        public bool SetBoatLocation(Location startLocation, Location endLocation, int length)
        {
            if (CheckBoatLocation(startLocation, endLocation, length))
            {
                Board[startLocation.Y - 1][startLocation.XAsInt - 1] = FieldType.BOAT;
                return true;
            }
            return false;
        }

        private bool CheckBoatLocation(Location startLocation, Location endLocation, int length)
        {
            for (int i = startLocation.Y - 1; i < endLocation.Y - 1; i++)
            {
                for (int j = startLocation.XAsInt - 1; j < endLocation.XAsInt - 1; j++)
                {
                    if (Board[i][j] != FieldType.WATER || !CheckSurroundingLocations(startLocation, endLocation, length) || !CheckValidBoatLength(startLocation, endLocation, length))
                    {
                        return false;
                    } 
                }
            }
            return true;
        }

        public bool CheckSurroundingLocations(Location startLocation, Location endLocation, int length)
        {
            if (!CheckValidBoatLength(startLocation, endLocation, length))
            {
                return false;
            } 
            
            if (IsHorizontal(startLocation, endLocation))
            {
                for (int i = startLocation.XAsInt - 2; i < endLocation.XAsInt; i++)
                {
                    if (i < 0 || i > GameFieldConstants.Size - 1) continue;
                    if (Board[startLocation.Y - 1][i] != FieldType.WATER)
                    {
                        return false;
                    }
                    if (startLocation.Y < GameFieldConstants.Size)
                    {
                        if (Board[startLocation.Y][i] != FieldType.WATER) return false;
                    }
                    if (startLocation.Y - 2 >= 0)
                    {
                        if (Board[startLocation.Y - 2][i] != FieldType.WATER) return false;
                    }
                }
                return true;
            } else
            {
                for (int i = startLocation.Y; i < endLocation.Y; i++)
                {
                    if (i < 0 || i > GameFieldConstants.Size - 1) continue;
                    if (Board[i][startLocation.XAsInt] != FieldType.WATER)
                    {
                        return false;
                    }
                    if (startLocation.XAsInt < GameFieldConstants.Size)
                    {
                        if (Board[i][startLocation.XAsInt] != FieldType.WATER) return false;
                    }
                    if (startLocation.XAsInt - 2 >= 0)
                    {
                        if (Board[i][startLocation.XAsInt] != FieldType.WATER) return false;
                    }
                }
                return true;
            }
            
        }

        private bool IsHorizontal(Location startLocation, Location endLocation)
        {
            return endLocation.Y == startLocation.Y;
        }

        public bool CheckValidBoatLength(Location startLocation, Location endLocation, int length)
        {
            return (endLocation.XAsInt - startLocation.XAsInt) + 1 == length && endLocation.Y == startLocation.Y
                || (endLocation.Y - startLocation.Y) + 1 == length && endLocation.XAsInt == startLocation.XAsInt;
        }

        public void InitializeArray()
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
    }
}
