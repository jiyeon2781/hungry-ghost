using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class PositionManager : MonoBehaviour
{
    private static PositionManager _instance;
    public static PositionManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] private int _itemCount = 3;
    [SerializeField] private int _positionCount = 5;

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
        List<int> posNum = MathUtility.MakeRandomNumbers(0, _positionCount - 1, _itemCount, true);
        for (int i = 0; i < _itemCount; i++)
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
            await UniTask.WaitUntil(()=> foods.Count < _itemCount);
            var rand = UnityEngine.Random.Range(0, _positionCount);
            while (foods.ContainsValue(rand))
                rand = UnityEngine.Random.Range(0, _positionCount);
            var food = Managers.PoolManager.Pop(positions[rand].transform);
            food.SetFoodPrefab();
            foods.Add(food, rand);
        }
    }
}
