using System;
using System.Collections.Generic;

namespace AddingPerformanceTests
{
    public class SumPerformanceTestFunctionsList
    {
        public static int ListSumEqualsAPlusB(List<int> intList, int startIndex, int endIndex)
        {
            int sum = 0;
            for (int i = startIndex; i < endIndex; i++)
            {
                sum = sum + intList[i];
            }
            return sum;
        }

        public static int ListSumPlusEquals(List<int> intList, int startIndex, int endIndex)
        {
            int sum = 0;
            for (int i = startIndex; i < endIndex; i++)
            {
                sum += intList[i];
            }
            return sum;
        }

        public static int ListSumForeach(List<int> intList, int startIndex, int endIndex)
        {
            int sum = 0;
            foreach (int i in intList.GetRange(startIndex, endIndex - startIndex))
            {
                sum += i;
            }
            return sum;
        }
    }
}
