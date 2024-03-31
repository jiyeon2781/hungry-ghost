using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _mouseSensitivity = 10000f;

    private float mouseY;
    private float mouseX;

    void Start()
    {
        Managers.InputManager.InputKeyAction -= OnInputKeyboard;
        Managers.InputManager.InputMouseAction -= OnInputMouse;
        Managers.InputManager.InputKeyAction += OnInputKeyboard;
        Managers.InputManager.InputMouseAction += OnInputMouse;
    }

    private void OnDestroy()
    {
        Managers.InputManager.InputKeyAction -= OnInputKeyboard;
        Managers.InputManager.InputMouseAction -= OnInputMouse;
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
    void OnInputMouse()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * _mouseSensitivity * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(0f, mouseX, 0f);
    }
}
