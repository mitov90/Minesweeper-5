﻿namespace Game.Tests
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
                { new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field() }, 
                { new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field() },
                { new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field() },
                { new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field() },
                { new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field(), new Field() },
            };

            Field[,] matrixInitialized = Board.PrepareMatrix(5, 10);
        }

        [TestMethod]
        public void EqualsFieldsTest()
        {
            Field first = new Field();
            Field second = new Field();

            bool equal = first.Equals(second);

            Assert.IsTrue(equal);
        }

        [TestMethod]
        public void EqualsFieldsNullableTest()
        {
            Field first = new Field();
            Field second = null;

            bool equal = first.Equals(second);

            Assert.IsFalse(equal);
        }

        private bool AreMatricesEqual(Field[,] first, Field[,] second)
        {
            if (first.GetLength(0) != second.GetLength(0))
            {
                return false;
            }

            if (first.GetLength(1) != second.GetLength(1))
            {
                return false;
            }

            for (int row = 0; row < first.GetLength(0); row++)
            {
                for (int col = 0; col < first.GetLength(1); col++)
                {
                    if (!first[row, col].Equals(second[row, col]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}