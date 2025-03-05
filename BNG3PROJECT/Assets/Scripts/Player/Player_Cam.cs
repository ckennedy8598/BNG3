using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_Cam : MonoBehaviour
{
    [Header("Sliders")]
    public Slider SensSliderX;
    public Slider SensSliderY;
    public TMP_Text SensTextX;
    public TMP_Text SensTextY;

    public float SensY;
    public float SensX;

    public Transform Orientation;

    float xRot;
    float yRot;

    private bool setRot;
    public bool DeathCamera;
    public bool CanMoveCamera;
    public bool PauseScript;

    [Header("Level Start Variable")]
    public float _levelStartRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        // Set and store sensitivity values
        if (!PlayerPrefs.HasKey("sensitivityX") && !PlayerPrefs.HasKey("sensitivityY"))
        {
            PlayerPrefs.SetFloat("sensitivityX", 21); PlayerPrefs.SetFloat("sensitivityY", 21);
            _loadSensitivity();
        }
        else
        {
            _loadSensitivity();
        }

        setRot = true;
        CanMoveCamera = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //SensSliderX.value = 20; SensSliderY.value = 20;

        // Rotate Camera + Player Orientation On Level Start
        yRot = _levelStartRotation;
    }

    // Update is called once per frame
    void Update()
    {
        _updateSliders();

        if (CanMoveCamera)
        {
            CameraMovement();
        }

        if (DeathCamera)
        {
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

    private void _updateSliders()
    {
        // Set sensitivity equal to slider value
        _saveSensitivity();
        SensX = SensSliderX.value;
        SensY = SensSliderY.value;

        if (SensSliderX != null && SensSliderY != null)
        {
            if (SensSliderX.value > 0)
            {
                SensTextX.text = SensSliderX.value.ToString("#");
            }
            else
            {
                SensTextX.text = "0";
            }

            if (SensSliderY.value > 0)
            {
                SensTextY.text = SensSliderY.value.ToString("#");
            }
            else
            {
                SensTextY.text = "0";
            }
        }
    }

    public bool SetCameraMovement()
    {
        CanMoveCamera = !CanMoveCamera;
        return CanMoveCamera;
    }

    private void _saveSensitivity()
    {
        PlayerPrefs.SetFloat("sensitivityX", SensSliderX.value);
        PlayerPrefs.SetFloat("sensitivityY", SensSliderY.value);
    }

    private void _loadSensitivity()
    {
        SensSliderX.value = PlayerPrefs.GetFloat("sensitivityX");
        SensSliderY.value = PlayerPrefs.GetFloat("sensitivityY");
    }
}
