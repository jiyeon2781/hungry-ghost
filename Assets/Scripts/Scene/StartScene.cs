using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        SceneType = Enums.Scene.Start;
        // TODO Init 
        Managers.DataManager.Init();
    }
}
