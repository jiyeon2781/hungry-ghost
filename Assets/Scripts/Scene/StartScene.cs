using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override async void Init()
    {
        SceneType = Enums.Scene.Start;

        await StartLobbyScene();
    }

    private async UniTask StartLobbyScene()
    {
        await UniTask.WaitUntil(() => Managers.IsInitialized);

        Managers.UIManager.ShowUI<StartSceneUI>();
        Managers.SoundManager.Play(Managers.GameManager.GameData.LobbyPathBGM, SoundManager.SoundType.BGM);
    }
}
