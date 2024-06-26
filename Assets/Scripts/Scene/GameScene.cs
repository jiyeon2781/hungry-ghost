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
        Managers.GameManager.Initialze();
        CreatePlayer();
    }

    async void CreatePlayer()
    {
        GameObject playerInstance;
        if (GameObject.Find("Player") == null)
            playerInstance = await Managers.ResourceManager.InstantiateInAsync(_playerAddress);
        else
            playerInstance = GameObject.Find("Player");

        SetPlayerPosition(playerInstance);
    }

    void SetPlayerPosition(GameObject playerInstance)
    {
        if (_playerStartPosition == null)
        {
            Debug.Log("[Game Scene] Player start position is not founded");
            return;
        }
        playerInstance.transform.position = _playerStartPosition.position;
    }
}
