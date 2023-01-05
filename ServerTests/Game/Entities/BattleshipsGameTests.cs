using Lib.Constants;
using Lib.GameEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void AddPlayerTest()
        {
            Player firstPlayer = new();
            Player secondPlayer = new();
            BattleshipsGame game = new(firstPlayer, "testGame");

            Assert.IsTrue(game.AddPlayer(secondPlayer));
        }

        [TestMethod()]
        public void GetOtherPlayerTest()
        {
            Player firstPlayer = new();
            Player secondPlayer = new();
            BattleshipsGame game = new("testGame");
            game.AddPlayer(firstPlayer);
            game.AddPlayer(secondPlayer);

            Assert.AreEqual(secondPlayer, game.GetOtherPlayer(firstPlayer));
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
