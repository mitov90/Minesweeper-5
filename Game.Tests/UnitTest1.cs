namespace Game.Tests
{
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
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) }, 
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) },
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) },
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) },
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) },
            };

            var matrixInitialized = Board.PrepareMatrix(5, 10);
            var result = this.AreMatricesEqual(expected, matrixInitialized);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PrepareMatrixSecondTest()
        {
            Field[,] expected =
            {
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) }, 
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) },
            };

            var matrixInitialized = Board.PrepareMatrix(2, 2);
            var result = this.AreMatricesEqual(expected, matrixInitialized);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PrepareMatrixFalseTest()
        {
            Field[,] expected =
            {
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) }, 
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) }, 
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) }, 
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) }, 
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) }, 
            };

            // different cols count
            var matrixInitialized = Board.PrepareMatrix(5, 5);
            var result = this.AreMatricesEqual(expected, matrixInitialized);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PrepareMatrixFalseSecondTest()
        {
            Field[,] expected =
            {
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) }, 
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) },
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) },
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) },
                { new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed), new Field(0, FieldStatus.Closed) },
            };

            // different rows count
            var matrixInitialized = Board.PrepareMatrix(6, 4);
            var result = this.AreMatricesEqual(expected, matrixInitialized);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EqualsFieldsTest()
        {
            var first = new Field(0, FieldStatus.Closed);
            var second = new Field(0, FieldStatus.Closed);

            var equal = first.Equals(second);

            Assert.IsTrue(equal);
        }

        [TestMethod]
        public void EqualsFieldsNullableTest()
        {
            var first = new Field(0 , FieldStatus.Closed);
            Field second = null;

            var equal = first.Equals(second);

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

            for (var row = 0; row < first.GetLength(0); row++)
            {
                for (var col = 0; col < first.GetLength(1); col++)
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
