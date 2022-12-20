using Lib.Constants;
using Lib.GameEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using server.Game.Entities;
using server.Handlers;
using Server.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace server.Game.Entities.Tests
{
    [TestClass()]
    public class BattleshipsGameTests
    {
        [TestMethod()]
        public void BattleshipsGameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void BattleshipsGameTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddPlayerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetOtherPlayerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetPlayersAsStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ShootTest()
        {
            Player player = new();
            Player otherPlayer = new();
            BattleshipsGame game = new("TestGame");
            game.AddPlayer(player);
            game.AddPlayer(otherPlayer);
            otherPlayer.GameFields[0][1][1] = FieldType.BOAT;
            Location location = new() { X = 'a', Y = 1 };

            Assert.IsTrue(game.Shoot(player, otherPlayer, location));
        }
    }
}
