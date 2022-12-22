using Lib.Constants;
using Lib.GameEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game.Entities.Tests
{
    [TestClass()]
    public class GameFieldTests
    {

        [TestMethod()]
        public void SetBoatLocationTest()
        {
            GameField field = new GameField();
            field.Board[0][0] = FieldType.BOAT;
            Location startLocation = new() { X = 'a', Y = 1 };
            Location endLocation = new() { X = 'e', Y = 1 };
            int length = 5;

            Assert.IsFalse(field.SetBoatLocation(startLocation, endLocation, length));
        }

        [TestMethod()]
        public void CheckSurroundingLocationsTest()
        {
            GameField gameField = new();
            gameField.Board[2][2] = FieldType.BOAT;
            gameField.Board[3][2] = FieldType.BOAT;
            Location startLocation = new() { X = 'c', Y = 3 };
            Location endLocation = new() { X = 'c', Y = 6 };
            int length = 5;

            Assert.IsFalse(gameField.CheckSurroundingLocations(startLocation, endLocation));
        }

        [TestMethod()]
        public void CheckValidBoatLengthTest()
        {
            Assert.Fail();
        }
    }
}