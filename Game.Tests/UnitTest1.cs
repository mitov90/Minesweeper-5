namespace GameTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PrepareMatrixTest()
        {
            Field[,] expected =
            {
                {new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field() }, 
                {new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field() },
                {new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field() },
                {new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field() },
                {new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field() },
            };

            Field[,] matrixInitialized = Board.PrepareMatrix(5, 10);

        }
    }
}
