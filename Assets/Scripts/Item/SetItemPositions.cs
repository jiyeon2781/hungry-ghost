using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SetItemPositions : MonoBehaviour
{
    private List<ItemPosition> positions;
    [SerializeField] private int SetItemCount = 3;
    Dictionary<int, Food> foods;

    private void Awake()
    {
        positions = GetComponentsInChildren<ItemPosition>().ToList();
        foods = new Dictionary<int, Food>();
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        List<int> posNum = MathUtility.MakeRandomNumbers(1, 1, 1);
        for (int i = 0; i < SetItemCount; i++)
        {
            var food = Managers.PoolManager.Pop(positions[posNum[i]].transform);
            foods.Add(posNum[i], food);
        }
    }
}
