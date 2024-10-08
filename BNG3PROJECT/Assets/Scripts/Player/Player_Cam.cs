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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
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
