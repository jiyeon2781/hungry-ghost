using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    void Start()
    {
        Init();
    }

    protected abstract void Init();
}
