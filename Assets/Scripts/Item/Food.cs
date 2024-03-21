using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IPoolable
{
    // public bool IsUsing { get => set => fa  lse; }
    private bool _isUsing;
    public bool IsUsing { get { return _isUsing; } set { _isUsing = value; } }

    private bool _isFavoriteFood;

    // Collider 충돌 진행 시 유령이 좋아하는 음식인지 아닌지
    // 좋아하는 음식이면 +, 싫어하는 음식이면 -

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
