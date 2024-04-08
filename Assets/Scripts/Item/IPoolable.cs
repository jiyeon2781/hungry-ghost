using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    ItemData CurrentItemData { get; set; }
    bool IsUsing { get; set; }
    virtual void OnPool() { }
    void InteractionPlayer() { }
}
