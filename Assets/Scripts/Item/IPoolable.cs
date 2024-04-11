using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    bool IsUsing { get; set; }
    Enums.Item ItemType { get; set; }
    virtual void OnPool() { }
    void InteractionPlayer() { }
}
