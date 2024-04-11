using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IPoolable
{
    [SerializeField] private float _rotationSpeed = 0.5f;
    [SerializeField] protected string _foodAddress = "Assets/Prefabs/Item/Foods/";

    protected bool _isUsing;
    protected ItemData _currentItemData;
    protected GameObject _food;
    protected Enums.Item _itemType;

    public bool IsUsing { get { return _isUsing; } set { _isUsing = value; } }
    public Enums.Item ItemType { get { return _itemType; } set { _itemType = value; } }

    public void Start()
    {
        PositionManager.Instance.OnDestroyFood -= PositionManager.Instance.PopFoodDictionary;
        PositionManager.Instance.OnDestroyFood += PositionManager.Instance.PopFoodDictionary;

        OnPool();
    }

    public async UniTask RotationFood()
    {
        await UniTask.WaitUntil(() => _food != null);
        while (_isUsing)
        {
            await UniTask.Yield();
            if (_food == null) break;
            _food.transform.Rotate(0, _rotationSpeed, 0);
        }
    }
    public virtual void OnPool() { }

    public virtual void InteractionPlayer(GameObject player)
    {
        if (player.CompareTag("Player") && _isUsing)
        {
            _isUsing = false;
            Destroy(_food);
            Managers.PoolManager.Push(gameObject.GetComponentInParent<Food>());
            PositionManager.Instance.OnDestroyFood(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        InteractionPlayer(other.gameObject);
    }
}
