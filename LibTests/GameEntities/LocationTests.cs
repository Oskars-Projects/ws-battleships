using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.GameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.GameEntities.Tests
{
    [TestClass()]
    public class LocationTests
    {
        [TestMethod()]
        public void FromStringTest()
        {
            Location location = new()
            {
                X = 'c',
                Y = 3
            };
            Assert.AreEqual(location, Location.FromString("c;3"));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Location location = new()
            {
                X = 'c',
                Y = 3
            };

            Assert.AreEqual(location.ToString(), "c;3");
        }

        [TestMethod()]
        public void EqualsTest()
        {
            Location location1 = new() { X='c', Y=3 };
            Location location2 = new() { X = 'c', Y = 3 };
            Assert.IsTrue(location1.Equals(location2));
        }

        [TestMethod()]
        public void XAsIntTest()
        {
            Location location = new() { X = 'c', Y = 3 };
            Assert.AreEqual(location.XAsInt, 3);
        }
    }
}