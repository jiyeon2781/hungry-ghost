using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    // bool IsUsing { get; set; }
    void OnPool() { }
}
