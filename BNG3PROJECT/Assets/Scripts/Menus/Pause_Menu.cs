using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_Menu : MonoBehaviour
{
    [Header("Player_Cam.cs Reference")]
    public Player_Cam PCam_Script;

    [Header("Pause Menu Items")]
    public GameObject P_MM;
    public GameObject P_Quit;
    public GameObject Crosshair;

    [Header("Death Menu Items")]
    private bool _keyPressed;
    public GameObject AnyKey_Text;
    public GameObject Death_Text;
    public bool PlayerDead;

    [Header("Pause Check")]
    public bool Paused;
    public void Start()
    {
        PlayerDead = false;
        Paused = false;
        PCam_Script = FindAnyObjectByType<Player_Cam>();
    }

    private void Update()
    {
        _onKeyPress();
    }

    private void _onKeyPress()
    {
        _deathScreen();

        if (Input.GetKeyDown(KeyCode.Escape) && (GetIsPaused() == false))
        {
            Paused = true;
            PCam_Script.CanMoveCamera = false;
            _unlockCursor();
            Time.timeScale = 0f;
            _pauseUI_Active();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (GetIsPaused() == true))
        {
            Paused = false;
            PCam_Script.CanMoveCamera = true;
            Time.timeScale = 1.0f;
            _lockCursor();
            _pauseUI_Deactive();
        }
    }

    private void _deathScreen()
    {
        if (PlayerDead)
        {
            Death_Text.SetActive(true);
            if (Input.anyKey && !_keyPressed)
            {
                PCam_Script.CanMoveCamera = false;
                PCam_Script.DeathCamera = true;
                _keyPressed = true;
                Paused = true;
                Time.timeScale = 1.0f;
                AnyKey_Text.SetActive(false);
                _unlockCursor();
                _deathUI_Active();
            }
            else if (_keyPressed == false)
            {
                AnyKey_Text.SetActive(true);
            }
            return;
        }
    }

    private void _unlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void _lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void _deathUI_Active()
    {
        P_MM.SetActive(true); P_Quit.SetActive(true);
    }
    
    private void _pauseUI_Active()
    {
        Crosshair.SetActive(false);
        P_MM.SetActive(true); P_Quit.SetActive(true);
    }

    private void _pauseUI_Deactive()
    {
        P_MM.SetActive(false); P_Quit.SetActive(false);
        Crosshair.SetActive(true);
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }

    public bool GetIsPaused()
    {
        return Paused;
    }
}
