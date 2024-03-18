using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    void Start()
    {
        Managers.InputManager.InputKeyAction -= OnInputKeyboard;
        Managers.InputManager.InputKeyAction += OnInputKeyboard;
    }

    void OnInputKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(_speed * Time.deltaTime * Vector3.forward);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(_speed * Time.deltaTime * Vector3.back);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(_speed * Time.deltaTime * Vector3.right);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(_speed * Time.deltaTime * Vector3.left);
    }
}
