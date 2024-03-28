using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {
    public Action InputKeyAction = null;
    public Action InputMouseAction = null;
    
    public void OnUpdate()
    {
        if (!Input.anyKey || !Managers.GameManager.IsGamePlaying) return;
        if (InputKeyAction != null) InputKeyAction.Invoke();
        if (InputMouseAction != null) InputMouseAction.Invoke();
    }
}
