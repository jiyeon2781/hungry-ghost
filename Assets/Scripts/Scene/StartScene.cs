using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartScene : BaseScene
{
    [SerializeField] private string pathBgm = "Assets/Sounds/BGM/Lobby.wav";
    protected override async void Init()
    {
        SceneType = Enums.Scene.Start;

        await StartLobbyScene();
    }

    private async UniTask StartLobbyScene()
    {
        await UniTask.WaitUntil(() => Managers.IsInitialized);

        Managers.UIManager.ShowUI<StartSceneUI>();
        Managers.SoundManager.Play(pathBgm, SoundManager.SoundType.BGM);
    }
}
