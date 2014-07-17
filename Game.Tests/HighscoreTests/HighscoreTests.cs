namespace Game.Tests.HighscoreTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper;
    using Minesweeper.Data;

    [TestClass]
    public class HighscoreTests
    {
        private Highscore highscore;

        [TestInitialize]
        public void InitializeHighScore()
        {
            highscore = new Highscore();
            Player firstPlayer = new Player("Gosho", 3);
            Player secondPlayer = new Player("Ivan", 4);
            Player thirdPlayer = new Player("Tony", 5);
            Player fourthPlayer = new Player("Sasho", 6);
            Player fifthPlayer = new Player("Mery", 7);
            Player sixthPlayer = new Player("Petq", 8);

            highscore.AddTopPlayer(firstPlayer);
            highscore.AddTopPlayer(secondPlayer);
            highscore.AddTopPlayer(thirdPlayer);
            highscore.AddTopPlayer(fourthPlayer);
            highscore.AddTopPlayer(fifthPlayer);
            highscore.AddTopPlayer(sixthPlayer);
        }

        [TestMethod]
        public void HighscoreCount()
        {
            Assert.AreEqual(highscore.TopPlayers.Count, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HighscoreAddNull()
        {
            highscore.AddTopPlayer(null);
        }

        [TestMethod]
        public void HighscoreIsHighScoreFalse()
        {
            Player notTopPlayer = new Player("Kalin", 1);
            Assert.IsFalse(highscore.IsHighScore(notTopPlayer.Score));
        }

        [TestMethod]
        public void HighscoreIsHighScoreTrue()
        {
            Player topPlayer = new Player("Kalina", 10);
            Assert.IsTrue(highscore.IsHighScore(topPlayer.Score));
        }

        [TestMethod]
        public void HighscoreSorting()
        {
            Player bestPlayer = new Player("Misho", 20);
            highscore.AddTopPlayer(bestPlayer);

            Assert.AreEqual(highscore.TopPlayers[0], bestPlayer);
        }
    }
}
