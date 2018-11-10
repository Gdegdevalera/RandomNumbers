using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Extensions
{
    public static class MatrixExtensions
    {
        public static int[] GetColumnWithDuplicate(this int[][] matrix)
        {
            return GetRowWithDuplicate(matrix.Transpose());
        }

        public static int[] GetRowWithDuplicate(this int[][] matrix)
        {
            return matrix.Select((x, i) => x.Distinct().Count() != x.Length ? i : -1)
                .Where(x => x != -1)
                .ToArray();
        }

        public static int[][] Transpose(this int[][] matrix)
        {
            if (matrix.Length == 0)
                return matrix;

            if (matrix.Any(x => x == null))
                throw new ArgumentException("Matrix can't contain null-row");

            if (matrix.Select(x => x.Length).Distinct().Count() > 1)
                throw new ArgumentException("All matrix's rows must have the same length");

            int w = matrix[0].Length;
            int h = matrix.Length;

            var result = new int[w][];

            for (int i = 0; i < w; i++)
            {
                result[i] = new int[h];
                for (int j = 0; j < h; j++)
                {
                    result[i][j] = matrix[j][i];
                }
            }

            return result;
        }
    }
}
