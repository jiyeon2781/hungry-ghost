using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IPoolable
{
    // public bool IsUsing { get => set => fa  lse; }
    private bool _isUsing;
    public bool IsUsing { get { return _isUsing; } set { _isUsing = value; } }

    private bool _isFavoriteFood;

    // Collider �浹 ���� �� ������ �����ϴ� �������� �ƴ���
    // �����ϴ� �����̸� +, �Ⱦ��ϴ� �����̸� -

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
