namespace Game.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Minesweeper;
    using Minesweeper.Data;
    using Minesweeper.Enums;

    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        [ExpectedException (typeof(ArgumentException))]
        public void TestInitialization()
        {
            var board = new Board(10, 10, -10);
        }

        [TestMethod]        
        public void TestInnerFieldMatrix()
        {
            var board = new Board(10, 9, 10);
            var rows = board.FieldsMatrix.GetLength(0);
            var cols = board.FieldsMatrix.GetLength(1);
            Assert.AreEqual(rows, 10);
            Assert.AreEqual(cols, 9);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestIndexerOutOfRange()
        {
            var board = new Board(5, 5, 10);
            var field = board.FieldsMatrix[-1, 0];            
        }

        [TestMethod]        
        public void TestFieldMatrixInitialization()
        {
            var board = new Board(5, 5, 10);
            var fieldMatrix = board.FieldsMatrix;
            bool check = AreAllFieldsClosed(fieldMatrix);
            Assert.IsTrue(check);
        }

        [TestMethod]
        public void TestMineSetter()
        {
            var board = new Board(5, 5, 10);
            var fieldMatrix = board.FieldsMatrix;
            var bombSetter = new MineSetterVisitor(new RandomGenerator());
            board.Accept(bombSetter);
            bool check = AreAllFieldsClosed(fieldMatrix);
            Assert.IsFalse(check);
        }

        private static bool AreAllFieldsClosed(Field[,] fieldMatrix)
        {
            Field checker = new Field(0, FieldStatus.Closed);

            for (int row = 0; row < fieldMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < fieldMatrix.GetLength(1); col++)
                {
                    if (!fieldMatrix[row, col].Equals(checker))
                    {
                        return false;
                    }
                }
            }

            return true;
        }        
    }
}