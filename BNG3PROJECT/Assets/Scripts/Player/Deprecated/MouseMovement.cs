using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float MouseSensitivity = 100f;

    private float _xRotation = 0f;
    private float _yRotation = 0f;
    void Start()
    {
        // Locks cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        // Rotate around the X axis (Look up and down)
        _xRotation -= mouseY;

        // Clamp the rotation
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        // Rotate around the Y axis (Look left and right)
        _yRotation += mouseX;

        // Apply rotations to our transform
        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }
}
