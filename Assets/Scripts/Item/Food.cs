using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IPoolable
{
    private bool _isUsing;
    public bool IsUsing { get { return _isUsing; } set { _isUsing = value; } }

    // Collider �浹 ���� �� ������ �����ϴ� �������� �ƴ���
    // �����ϴ� �����̸� +, �Ⱦ��ϴ� �����̸� -

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // TODO ���� Ǯ�� ����������(deactivated)
            Debug.Log("�÷��̾�� �꿴���!");
        }
    }
}
