using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Extensions
{
    public static class ArrayExtensions
    {
        public static int[] Search(this int[] array, int value)
        {
            return array.Select((x, i) => x == value ? i : -1).Where(x => x != -1).ToArray();
        }

        public static int[] Search(this int[] array, int[] value)
        {
            var result = new List<int>();

            for (int i = 0; i < array.Length - value.Length; i++)
            {
                int j;
                for (j = 0; j < value.Length; j++)
                {
                    if (array[i + j] != value[j])
                    {
                        break;
                    }
                }

                if (j == value.Length)
                {
                    result.Add(i);
                }
            }

            return result.ToArray();
        }
    }
}
