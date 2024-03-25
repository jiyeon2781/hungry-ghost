using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] private string _playerAddress = "Assets/Prefabs/GhostPlayer/Player.prefab";
    [SerializeField] private string _itemAddress = "Assets/Prefabs/Item/Food.prefab";
    [SerializeField] private string _positionAddress = "Assets/Prefabs/Item/ItemPositions.prefab";

    [SerializeField] private Transform _playerStartPosition;

    private GameObject _positions;
    protected override void Init()
    {
        SceneType = Enums.Scene.InGame;
        CreatePlayer();
        CreateItemPoolAndPosition();
        Managers.GameManager.Initialze();
    }

    void CreateItemPoolAndPosition()
    {
        Managers.ResourceManager.LoadAsync(_itemAddress, false, obj =>
        {
            Managers.PoolManager.InitFoodPool(obj.gameObject);
            CreateItemPosition();
        }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{_itemAddress}\" GameObject"));
    }

    void CreateItemPosition()
    {
        Managers.ResourceManager.LoadAsync(_positionAddress, false, obj =>
       {
           _positions = obj.gameObject;
           Instantiate(_positions);
       }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{_positionAddress}\" GameObject"));
    }

    void CreatePlayer()
    {
        GameObject playerInstance;
        if (GameObject.Find("Player") == null)
        {
            Managers.ResourceManager.LoadAsync(_playerAddress, false, obj =>
            {
                playerInstance = Instantiate(obj);
                SetPlayerPosition(playerInstance);
            }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{_playerAddress}\" GameObject"));
        }
        else
        {
            playerInstance = GameObject.Find("Player");
            SetPlayerPosition(playerInstance);
        }
    }

    void SetPlayerPosition(GameObject playerInstance)
    {
        if (_playerStartPosition == null)
        {
            Debug.Log("Player의 Position을 지정해주세요.");
            return;
        }
        playerInstance.transform.position = _playerStartPosition.position;
    }
}
