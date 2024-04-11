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

    [SerializeField] private int _favoriteItemCount = 5;
    [SerializeField] private int _hateItemCount = 3;
    [SerializeField] private int _positionCount = 5;

    public int FavoriteItemCount { get { return _favoriteItemCount; } set { _favoriteItemCount = value; } }
    public int HateItemCount { get { return _hateItemCount; } set { _hateItemCount = value; } }

    private int _itemCount;
    private int rand;

    private List<ItemPosition> positions;
    private static Dictionary<Food, int> foods;

    public Action<Food> OnDestroyFood;

    private void Awake()
    {
        _instance = this;
        positions = GetComponentsInChildren<ItemPosition>().ToList();
        foods = new();
        _itemCount = _hateItemCount + _favoriteItemCount;
        rand = UnityEngine.Random.Range(0, _positionCount);
    }

    private async void Start()
    {
        await SetItemsPlayingGame();
    }

    private void Init()
    {
        List<int> posNum = MathUtility.MakeRandomNumbers(0, _positionCount - 1, _itemCount, true);
        var itemType = Enums.Item.Favorite;
        for (int i = 0; i < _itemCount; i++)
        {
            if (_favoriteItemCount == i) itemType = Enums.Item.Hate;
            var food = Managers.PoolManager.Pop(positions[posNum[i]].transform, itemType);
            foods.Add(food, posNum[i]);
        }
    }

    public void PopFoodDictionary(Food food)
    {
        foods.Remove(food);
    }

    public async UniTask SetItemsPlayingGame()
    {
        Init();

        await UniTask.WaitForSeconds(0.1f);

        while (Managers.GameManager.IsGamePlaying)
        {
            await UniTask.WaitUntil(()=> foods.Count < _itemCount);
            await UniTask.WaitForSeconds(5f);
            // 텀을 두고 생성
            if (!Managers.GameManager.IsGamePlaying) break;

            while (foods.ContainsValue(rand))
                rand = UnityEngine.Random.Range(0, _positionCount);
            var food = Managers.PoolManager.Pop(positions[rand].transform, (Enums.Item) UnityEngine.Random.Range(0, (int) Enums.Item.MaxCount));
            food.OnPool();

            foods.Add(food, rand);
        }
    }
}
