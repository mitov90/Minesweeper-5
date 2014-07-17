namespace Game.Tests.PlayerTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper;
    using Minesweeper.Data;
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlayerNullName()
        {
            Player player = new Player(null, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayerNegativeScore()
        {
            Player player = new Player("Gosho", -3);
        }

        [TestMethod]
        public void PlayerCompare()
        {
            Player firstPlayer = new Player("Gosho", 2);
            Player secondPlayer = new Player("Gosho", 1);
            Assert.AreEqual(firstPlayer.CompareTo(secondPlayer), -1);
        }
    }
}
