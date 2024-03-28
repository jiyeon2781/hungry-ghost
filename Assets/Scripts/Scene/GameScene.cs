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
        Managers.GameManager.Initialze();
    }

    async void CreatePlayer()
    {
        GameObject playerInstance;
        if (GameObject.Find("Player") == null)
        {
            playerInstance = await Managers.ResourceManager.InstantiateInAsync(_playerAddress);
            SetPlayerPosition(playerInstance);
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
