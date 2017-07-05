namespace EasySharp.NHelpers.CustomExtensionMethods
{
    using System.Linq;

    public static class TwodimensionalArrayHelper
    {
        public static int[][] MultiplyBy(this int[][] leftMatrix, int[][] rightMatrix)
        {
            if (leftMatrix[0].Length != rightMatrix.Length)
            {
                return null; // Matrices are of incompatible dimensions
            }

            return leftMatrix.Select( // goes through <leftMatrix matrix> row by row

                    (leftMatrixRow, leftMatrixRowIndexThatIsNotUsed) =>

                        rightMatrix[0].Select( // goes through first row of <rightMatrix> cell by cell

                                (rightFirstRow, rightMatrixColumnIndex) =>

                                    rightMatrix
                                        .Select(rightRow => rightRow[rightMatrixColumnIndex]) // selects column from <rightMatrix> for <rightMatrixColumnIndex>
                                        .Zip(leftMatrixRow, (rowCell, columnCell) => rowCell * columnCell) // does scalar product
                                        .Sum() // computes the sum of the products (rowCell * columnCell) sequence.
                            )
                            .ToArray() // the new cell within computed matrix
                )
                .ToArray(); // the computed matrix itself
        }
    }

    /*
        Here are some test values:

        // Test1
        int[][] A = { new[] { 1, 2, 3 } };
        int[][] B = { new[] { 1 }, new[] { 2 }, new[] { 3 } };
        int[][] result = A.MultiplyBy(B);

        // Test2
        int[][] A = { new[] { 1 }, new[] { 2 }, new[] { 3 } };
        int[][] B = { new[] { 1, 2, 3 } };
        int[][] result = A.MultiplyBy(B);

        // Test3
        int[][] A = new int[][] { new[] { 1, 2 }, new[] { 2, 2 }, new[] { 3, 1 } };
        int[][] B = new int[][] { new[] { 1, 1, 1 }, new[] { 2, 3, 2 } };
        int[][] result = A.MultiplyBy(B);
    */
}
