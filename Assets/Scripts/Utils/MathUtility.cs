using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtility
{
    private static List<int> result, numbers;
    public static List<int> MakeRandomNumbers(int minValue, int maxValue, int count, bool isDuplicate = false)
    {
        result = new();
        numbers = new();

        for (int i = 0; i < count ; i++)
        {
            var rand = Random.Range(minValue, maxValue + 1);
            if (isDuplicate)
            {
                while (numbers.Contains(rand))
                    rand = Random.Range(minValue, maxValue + 1);
                numbers.Add(rand);
            }
            result.Add(rand);
        }

        return result;
    }
}
