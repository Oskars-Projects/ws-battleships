using Lib.Constants;
using Newtonsoft.Json;
using Server.Game.Entities;

namespace ConsoleClient.ConsoleHelper
{
    public static class GameFieldConsoleHelper
    {
        private static string[] s_LETTERS = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };


        public static void PrintGameFields(GameField[] gameFields)
        {

            if (gameFields.Length == 0)
                return;

            // build header

            Console.Write("  ");
            // for every array
            for (int i = 0; i < gameFields.Length; i++)
            {
                for (int x = 0; x < gameFields[i].Board.Length; x++)
                {
                    Console.Write(i + " ");
                }
                Console.Write("  ");
            }

            Console.WriteLine();

            // Loop for every row
            for(int y = 0; y < GameFieldConstants.Size; y++)
            {
                Console.Write(s_LETTERS[y] + " ");

                // Loop for every array
                for (int j = 0; j < gameFields.Length; j++)
                {
                    // Loop for every column
                    for(int x = 0; x < GameFieldConstants.Size; x++)
                    {

                        FieldType val = (FieldType) gameFields[j][y][x];
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
