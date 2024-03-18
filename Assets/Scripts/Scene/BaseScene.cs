using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public Enums.Scene SceneType { get; protected set; } = Enums.Scene.Default;
    void Start()
    {
        Init();
    }

    protected abstract void Init();
}
