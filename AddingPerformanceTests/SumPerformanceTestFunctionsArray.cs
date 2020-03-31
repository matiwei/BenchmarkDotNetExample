using System;
using System.Collections.Generic;
using System.Text;

namespace AddingPerformanceTests
{
    class SumPerformanceTestFunctionsArray
    {
        public static int ArraySumAEqualsAPlusB(int[] intArray, int startIndex, int endIndex)
        {
            int sum = 0;
            for (int i = startIndex; i < endIndex; i++)
            {
                sum = sum + intArray[i];
            }
            return sum;
        }

        public static int ArraySumAPlusEqualsB(int[] intArray, int startIndex, int endIndex)
        {
            int sum = 0;
            for (int i = startIndex; i < endIndex; i++)
            {
                sum += intArray[i];
            }
            return sum;
        }

        public static int ArraySumForeach(int[] intArray, int startIndex, int endIndex)
        {
            int sum = 0;
            foreach (int i in intArray[startIndex..endIndex])
            {
                sum += i;
            }
            return sum;
        }

        public static int ArraySumForeachSpan(int[] intArray, int startIndex, int endIndex)
        {
            int sum = 0;
            foreach (int i in new Span<int>(intArray, startIndex, endIndex - startIndex))
            {
                sum += i;
            }
            return sum;
        }

        public static int ArraySumSpan(int[] intArray, int startIndex, int endIndex)
        {
            int sum = 0;
            var x = new Span<int>(intArray, startIndex, endIndex - startIndex);
            for (int i = 0; i < x.Length; i++)
            {
                sum += x[i];
            }
            return sum;
        }
    }
}
