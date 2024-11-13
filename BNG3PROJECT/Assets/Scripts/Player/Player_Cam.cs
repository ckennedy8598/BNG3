using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Cam : MonoBehaviour
{
    public float SensY;
    public float SensX;

    public Transform Orientation;

    float xRot;
    float yRot;

    private bool setRot;
    public bool DeathCamera;
    public bool CanMoveCamera;
    public bool PauseScript;
    // Start is called before the first frame update
    void Start()
    {
        setRot = true;
        CanMoveCamera = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMoveCamera)
        {
            CameraMovement();
        }

        if (DeathCamera)
        {
            Debug.Log("STATE OF DEATHCAMERA: " + DeathCamera);
            DeathCameraStart();
        }
    }
    public void CameraMovement()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * SensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * SensY;

        yRot += mouseX;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        Orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }

    private void DeathCameraStart()
    {
        _setRotationDeathOnce();
        transform.Rotate(0f, 20 * Time.deltaTime, 0f, Space.Self);
    }

    private void _setRotationDeathOnce()
    {
        if (setRot)
        {
            transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
            setRot = false;
        }
    }

    public bool SetCameraMovement()
    {
        CanMoveCamera = !CanMoveCamera;
        return CanMoveCamera;
    }
}
