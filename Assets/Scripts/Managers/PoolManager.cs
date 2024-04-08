using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager
{
    public GameObject GameObject { get; private set; }
    public Transform MainRoot { get; set; }
    public List<Transform> RootTransform { get; set; }

    private Queue<Food> _poolFoodQueue = new();
    // Queue -> Dic?
    private Dictionary<Enums.Item, Queue<Food>> _poolDic = new();
    public void Init()
    {
        if (MainRoot != null) return;
        MainRoot = new GameObject { name = "--Pool_Root" }.transform;
        Object.DontDestroyOnLoad(MainRoot);

        RootTransform = new List<Transform>();
    }

    public void InitFoodPool(GameObject original, int count = 5) // Food Pool 초기 생성
    {
        Init();

        GameObject = original;

        RootTransform.Add(new GameObject().transform);
        RootTransform[RootTransform.Count - 1].name = $"--{original.name}_Root";
        RootTransform[RootTransform.Count - 1].SetParent(MainRoot);

        for (int i = 0; i < count; i++)
            Push(Create());
    }

    Food Create() // Object 생성
    {
        GameObject obj = Object.Instantiate<GameObject>(GameObject);
        obj.name = GameObject.name;
        return obj.GetOrAddComponent<Food>();
    }

    public void Push(Food food) // 풀에 Push
    {
        if (food == null) return;

        food.transform.parent = RootTransform[(int) food.ItemType];
        food.transform.localPosition = Vector3.zero;
        food.gameObject.SetActive(false);
        food.IsUsing = false;

        if (_poolDic.ContainsKey(food.ItemType))
        {
            _poolDic.TryGetValue(food.ItemType, out _poolFoodQueue);
            _poolFoodQueue.Enqueue(food);
            return;
        }

        _poolDic.Add(food.ItemType, new Queue<Food>());
    }

    public Food Pop(Transform parent, Enums.Item itemType = Enums.Item.Favorite) // 풀에서 꺼내오기
    {
        Food food;

        if (_poolDic[itemType].Count > 0) food = _poolDic[itemType].Dequeue();
        else food = Create();

        if (parent == null)
            food.transform.parent = Managers.GameSceneManager.CurrentScene.transform;

        food.gameObject.SetActive(true);
        food.transform.SetParent(parent);
        food.transform.localPosition = Vector3.zero;
        food.IsUsing = true;

        return food;
    }

    public void Clear()
    {
        foreach(Transform child in RootTransform) GameObject.Destroy(child.gameObject);
        _poolDic.Clear();
    }
}