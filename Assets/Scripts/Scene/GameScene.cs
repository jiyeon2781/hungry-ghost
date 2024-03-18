using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] private string _playerAddress = "Assets/Prefabs/GhostPlayer/Player.prefab";
    [SerializeField] private Transform _playerStartPosition;
    protected override void Init()
    {
        SceneType = Enums.Scene.InGame;
        CreatePlayer();
    }

    void CreatePlayer()
    {
        if (GameObject.Find("Player") == null)
        {
            Managers.ResourceManager.LoadAsync(_playerAddress, false, obj =>
            {
                GameObject playerInstance = Instantiate(obj);
                SetPlayerPosition(playerInstance);
            }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{_playerAddress}\" GameObject"));
        }
        else
        {
            GameObject playerInstance = GameObject.Find("Player");
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
