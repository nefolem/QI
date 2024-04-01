using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private float _cameraVerticalRotation = 0f;
    [SerializeField] private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instanсe.IsCardShown)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            RotateWithMouse();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void RotateWithMouse()
    {
        float inputX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _cameraVerticalRotation -= inputY;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * _cameraVerticalRotation;
        _player.Rotate(Vector3.up * inputX);
    }
}
