using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cam : MonoBehaviour
{
    public float SensY;
    public float SensX;

    public Transform Orientation;

    float xRot;
    float yRot;

    private bool _canMoveCamera = true;
    public bool PauseScript;
    // Start is called before the first frame update
    void Start()
    {
        //PauseScript = GameObject.Find("User_Interface").GetComponent<Pause_Menu>().GetIsPaused();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScript = GameObject.Find("User_Interface").GetComponent<Pause_Menu>().GetIsPaused();
            if (PauseScript)
            {
                _canMoveCamera = true;
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log("Cursor Lock State: " + Cursor.lockState);
                Cursor.visible = true;
                return;
            }
            else if (!PauseScript)
            {
                _canMoveCamera = false;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("Cursor Lock State: " + Cursor.lockState);
                Cursor.visible = false;
            }
        }
        if (_canMoveCamera)
        {
            CameraMovement();
        }
        //CameraMovement();
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
}
