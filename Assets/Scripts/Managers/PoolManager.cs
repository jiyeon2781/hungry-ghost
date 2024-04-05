using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager
{
    public GameObject GameObject { get; private set; }
    public Transform RootTransform { get; set; }

    private Queue<Food> _poolFoodQueue = new();

    public void InitFoodPool(GameObject original, int count = 5) // Food Pool 초기 생성
    {
        GameObject = original;
        RootTransform = new GameObject().transform;
        RootTransform.name = $"--{original.name}_Root";

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
        food.transform.localPosition = Vector3.zero;
        food.gameObject.SetActive(false);
        food.IsUsing = false;

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
        food.transform.SetParent(parent);
        food.transform.localPosition = Vector3.zero;
        food.IsUsing = true;

        return food;
    }

    public void Clear()
    {
        foreach(Transform child in RootTransform) GameObject.Destroy(child.gameObject);
        _poolFoodQueue.Clear();
    }
}