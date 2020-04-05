using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalSolver
{
    public static class ArrayExtensions
    {
        public static IEnumerable<(int x, T value)> IterateX<T>(this T[,] array, int y)
        {
            for (int x = 0; x < array.GetUpperBound(0) + 1; x++)
            {
                yield return (x, array[x, y]);
            }
        }

        public static IEnumerable<(int y, T value)> IterateY<T>(this T[,] array, int x)
        {
            for (int y = 0; y < array.GetUpperBound(0) + 1; y++)
            {
                yield return (y, array[x, y]);
            }
        }

        public static IEnumerable<(int x, int y, T value)> IterateAll<T>(this T[,] array)
        {
            for (int x = 0; x < array.GetUpperBound(0) + 1; x++)
            {
                for (int y = 0; y < array.GetUpperBound(1) + 1; y++)
                {
                    yield return (x, y, array[x, y]);
                }
            }
        }

        public static T[,] Transpose<T>(this T[,] array)
        {
            var height = array.GetUpperBound(1) + 1;
            var width = array.GetUpperBound(0) + 1;
            var result = new T[width, height];
            foreach (var c in array.IterateAll())
            {
                result[c.y, c.x] = c.value;
            }
            return result;
        }
    }
}
