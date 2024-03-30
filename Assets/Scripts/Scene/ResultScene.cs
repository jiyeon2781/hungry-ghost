using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScene : BaseScene
{
    protected override void Init()
    {
        SceneType = Enums.Scene.Result;
        var ui = Managers.UIManager.ShowUI<GameOverUI>();
    }
}