using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {
    public Action InputKeyAction = null;
    
    public void OnUpdate()
    {
        if (!Input.anyKey) return;
        if (InputKeyAction != null) InputKeyAction.Invoke();
    }
}
