using System;
using System.Collections.Generic;

namespace Number_of_Submatrices_That_Sum_to_Target
{
  class Program
  {
    static void Main(string[] args)
    {
      var matrix = new int[3][] { new int[] { 0, 1, 0 }, new int[] { 1, 1, 1 }, new int[] { 0, 1, 0 } };
      Solution s = new Solution();
      var answer = s.NumSubmatrixSumTarget(matrix, target: 4);
      Console.WriteLine(answer);
    }
  }

  public class Solution
  {
    public int NumSubmatrixSumTarget(int[][] matrix, int target)
    {
      // step 1 - calculate the row prefix sum
      // O(row)
      int result = 0;
      int row = matrix.Length;
      int column = matrix[0].Length;
      for (int r = 0; r < row; r++)
      {
        for (int c = 1; c < column; c++)
        {
          matrix[r][c] += matrix[r][c - 1];
        }
      }

      // consider two adjacent column, Why ? as we need to find all subset matrix
      // O(column * column)
      for (int c1 = 0; c1 < column; c1++)
      {
        for (int c2 = c1; c2 < column; c2++)
        {
          Dictionary<int, int> kvp = new Dictionary<int, int>();
          int sum = 0;
          // now perform 560. Subarray Sum Equals K this approach  
          for (int r = 0; r < row; r++)
          {
            sum += matrix[r][c2] - (c1 > 0 ? matrix[r][c1 - 1] : 0);
            if (sum == target)
            {
              result++;
            }
            int difference = sum - target;
            if (kvp.ContainsKey(difference)) 
            { 
              result += kvp[difference];
            }
            if (!kvp.ContainsKey(sum)) kvp.Add(sum, 0);
            kvp[sum]++;
          }
        }
      }
      // total time complexity - O(row * column * column)
      // space - O(N)
      return result;
    }
  }
}
