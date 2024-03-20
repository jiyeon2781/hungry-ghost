using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager
{
    #region Food Pool Class
    class FoodPool
    {
        public GameObject GameObject { get; private set; }
        public Transform RootTransform { get; set; }

        private Queue<Food> _poolFoodQueue = new Queue<Food>();
        public void Init(GameObject original, int count) // 풀 초기화
        {
            GameObject = original;
            RootTransform = new GameObject().transform;
            RootTransform.name = $"{original.name}Root";

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

            food.transform.parent = RootTransform;
            food.gameObject.SetActive(false);
            food.IsActivated = false;

            _poolFoodQueue.Enqueue(food);
        }

        public Food Pop(Transform parent) // 풀에서 꺼내오기
        {
            Food food;

            if (_poolFoodQueue.Count > 0) food = _poolFoodQueue.Dequeue();
            else food = Create();
            if (parent == null)
                food.transform.parent = Managers.GameSceneManager.CurrentScene.transform;
            food.gameObject.SetActive(true);
            food.transform.parent = parent;
            food.IsActivated = true;

            return food;
        }
    }
    #endregion

    private Dictionary<string, FoodPool> _foodPool = new Dictionary<string, FoodPool>();
    private Transform _root;

    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "--RootPool" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreateFoodPool(GameObject original, int count)
    {
        FoodPool foodPool = new FoodPool();
        foodPool.Init(original, count);

        _foodPool.Add(original.name, foodPool);
    }

    public void Push(Food food)
    {
        string name = food.gameObject.name;
        if (!_foodPool.ContainsKey(name))
        {
            GameObject.Destroy(food.gameObject);
            return;
        }
        _foodPool[name].Push(food);
    }

    public GameObject GetOriginalObject(string name)
    {
        if (!_foodPool.ContainsKey(name)) return null;
        return _foodPool[name].GameObject;
    }

    public void Clear()
    {
        foreach(Transform child in _root) GameObject.Destroy(child.gameObject);
        _foodPool.Clear();
    }
}
