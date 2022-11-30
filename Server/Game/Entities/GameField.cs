using Lib.Constants;
using Lib.Extensions;
using Lib.GameEntities;
using System.IO.IsolatedStorage;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace Server.Game.Entities
{
    public class GameField
    {
        public int[][] Board = new int[GameFieldConstants.Size][];

        public GameField()
        {
            InitializeArray();
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
            for (int i = 0; i < endLocation.Y - startLocation.Y; i++)
            {
                for (int j = 0; j < endLocation.XAsInt - startLocation.XAsInt; j++)
                {
                    if (Board[i][j] != FieldType.WATER || !CheckSurroundingLocations(startLocation, endLocation) || CheckValidBoatLength(startLocation, endLocation, length))
                    {
                        return false;
                    } 
                }
            }
            return true;
        }

        private bool CheckSurroundingLocations(Location startLocation, Location endLocation)
        {
            for (int i = 0; i < endLocation.Y - startLocation.Y; i++)
            { 
                for (int j = 0; j < endLocation.XAsInt - startLocation.XAsInt; j++)
                {
                    // checks above, below and next to location
                    if (Board[startLocation.Y + i - 1][startLocation.XAsInt + j] != FieldType.WATER 
                       || Board[startLocation.Y + i + 1][startLocation.XAsInt + j] != FieldType.WATER
                       || Board[startLocation.Y + i][startLocation.XAsInt + j - 1] != FieldType.WATER
                       || Board[startLocation.Y + i][startLocation.XAsInt + j + 1] != FieldType.WATER)
                    {
                        // checks diagonals of startLocation
                        if (Board[startLocation.Y - 1][startLocation.XAsInt - 1] != FieldType.WATER 
                           || Board[startLocation.Y + 1][startLocation.XAsInt - 1] != FieldType.WATER
                           || Board[startLocation.Y + 1][startLocation.XAsInt + 1] != FieldType.WATER
                           || Board[startLocation.Y - 1][startLocation.XAsInt - 1] != FieldType.WATER
                           //checks diagonals of endLocation
                           || Board[endLocation.Y - 1][endLocation.XAsInt - 1] != FieldType.WATER
                           || Board[endLocation.Y + 1][endLocation.XAsInt - 1] != FieldType.WATER
                           || Board[endLocation.Y + 1][endLocation.XAsInt + 1] != FieldType.WATER
                           || Board[endLocation.Y - 1][endLocation.XAsInt - 1] != FieldType.WATER)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool CheckValidBoatLength(Location startLocation, Location endLocation, int length)
        {
            return (Math.Abs(endLocation.XAsInt - startLocation.XAsInt) + 1 == length && endLocation.Y == startLocation.Y)
                || (Math.Abs(endLocation.Y - startLocation.Y) + 1 == length && endLocation.XAsInt == startLocation.XAsInt);
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
    }
}
