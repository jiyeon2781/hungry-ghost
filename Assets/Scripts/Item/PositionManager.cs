using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using System;

public class PositionManager : MonoBehaviour
{
    private static PositionManager _instance;
    public static PositionManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] private int _setItemCount = 3;

    private List<ItemPosition> positions;
    private static Dictionary<Food, int> foods;

    public Action<Food> OnDestroyFood;

    private void Awake()
    {
        _instance = this;
        positions = GetComponentsInChildren<ItemPosition>().ToList();
        foods = new();

        OnDestroyFood -= PopFoodDictionary;
        OnDestroyFood += PopFoodDictionary;
    }

    private async void Start()
    {
        await SetItems();
    }

    void Init()
    {
        List<int> posNum = MathUtility.MakeRandomNumbers(0, 4, _setItemCount, true);
        for (int i = 0; i < _setItemCount; i++)
        {
            var food = Managers.PoolManager.Pop(positions[posNum[i]].transform);
            foods.Add(food, posNum[i]);
        }
    }

    public void PopFoodDictionary(Food food)
    {
        foods.Remove(food);
    }

    public async UniTask SetItems()
    {
        Init();
        Debug.Log("세팅 완료!");
        await UniTask.Delay(1000);

        while (Managers.GameManager.IsGamePlaying)
        {
            if (foods.Count == _setItemCount) continue;
            // TODO 아이템이 빠졌을 때 주기적으로 세팅
        }
    }
}
