using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTests.Game.Entities
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void HasBoatsZoPlaceTest()
        {
            List<int> BoatsToPlace = new List<int> {3, 3, 2, 2};
            Assert.IsTrue(BoatsToPlace.Any());
        }
    }
}
