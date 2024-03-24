using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class PositionManager : MonoBehaviour
{
    private List<ItemPosition> positions;
    [SerializeField] private int SetItemCount = 3;
    Dictionary<int, Food> foods;

    // Position 관리 및 Food Prefab 설정

    private void Awake()
    {
        positions = GetComponentsInChildren<ItemPosition>().ToList();
        foods = new Dictionary<int, Food>();
    }

    private async void Start()
    {
        await SetItems();
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

    public async UniTask SetItems()
    {
        Init();
        await UniTask.Delay(1000);
        // TODO 아이템이 빠졌을 때 주기적으로 세팅
        Debug.Log("세팅 완료!");
    }
}
