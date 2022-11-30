using Lib.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.ConsoleHelper
{
    public static class GameFieldConsoleHelper
    {

        public static void PrintGameFields(int[][] gameFields)
        {

            if (gameFields[0] == null)
                return;

            if (gameFields[0].Length != Math.Pow(GameFieldConstants.Size, 2))
            {
                Console.WriteLine("GameField has invalid length!");
                return;
            }

            // Loop for every row
            for(int y = 0; y < GameFieldConstants.Size; y++)
            {
                // Loop for every array
                for (int j = 0; j < gameFields.GetLength(0); j++)
                {
                    // Loop for every column
                    for(int x = 0; x < GameFieldConstants.Size; x++)
                    {
                        int val = gameFields[j][y * GameFieldConstants.Size + x];
                        Console.BackgroundColor = val switch
                        {
                            FieldType.WATER => ConsoleColor.Blue,
                            FieldType.BOAT => ConsoleColor.DarkGray,
                            FieldType.HIT => ConsoleColor.Red,
                            FieldType.NOHIT => ConsoleColor.DarkYellow,
                            _ => throw new ArgumentOutOfRangeException("Invalid FieldType: " + val),
                        };
                        Console.Write("  ");
                        Console.ResetColor();
                    }
                    Console.Write("\t");
                }
                Console.WriteLine();
            }


        }

    }
}
