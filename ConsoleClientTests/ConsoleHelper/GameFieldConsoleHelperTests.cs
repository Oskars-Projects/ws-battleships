using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleClient.ConsoleHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.ConsoleHelper.Tests
{
    [TestClass()]
    public class GameFieldConsoleHelperTests
    {
        [TestMethod()]
        public void PrintGameFieldsTest()
        {
            Random random = new();

            int[][] gameFields = new int[2][];
            for (int i = 0; i < 2; i++)
            {
                int[] values = new int[100];
                for(int j = 0; j < 100; j++)
                {
                    values[j] = random.Next(0, 4);
                }
                gameFields[i] = values;
            }
            GameFieldConsoleHelper.PrintGameFields(gameFields);

            Assert.IsTrue(true);
        }
    }
}