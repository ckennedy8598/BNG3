using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_Menu : MonoBehaviour
{
    [Header("Player_Cam.cs Reference")]
    public Player_Cam PCam_Script;

    [Header("Player Weapon")]
    public GameObject PWeapon;

    
    [Header("Pause Menu Items")]
    public GameObject P_MM;
    public GameObject P_Retry;
    public GameObject P_Options;
    public GameObject P_Back;
    public GameObject P_Quit;
    public GameObject Crosshair;
    public GameObject SliderHolder;
    public GameObject Options_Controls;
    public GameObject Options_ControlsBack;
    public GameObject Options_ControlsDisplay;
    

    [Header("New Pause Menu Items")]
    public GameObject EscapeMenu_Top;
    public GameObject EscapeMenu_Options;
    public GameObject EscapeMenu_Controls;

    [Header("Death Menu Items")]
    private bool _keyPressed;
    public GameObject AnyKey_Text;
    public GameObject Death_Text;
    public bool PlayerDead;

    [Header("Pause Check")]
    public bool Paused;

    [Header("UI Ability Elements")]
    public GameObject DashUI;
    public GameObject SoulOverflowUI;
    public GameObject AshesUI;

    public GameObject GameMaster;
    public GameMaster GameMasterScript;
    public void Start()
    {
        PlayerDead = false;
        Paused = false;
        PCam_Script = FindAnyObjectByType<Player_Cam>();

        GameMaster = GameObject.Find("Game Master");
        GameMasterScript = GameMaster.GetComponent<GameMaster>();
    }

    private void Update()
    {
        _onKeyPress();
        _checkCollected();

        /*
        if (GameMasterScript = null)
        {
            GameMasterScript = GameMaster.GetComponent<GameMaster>();
        }*/
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
            PWeapon.SetActive(false); Crosshair.SetActive(false);
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
        Crosshair.SetActive(false);
        EscapeMenu_Top.SetActive(true);
        P_Options.SetActive(false);
    }
    
    private void _pauseUI_Active()
    {
        EscapeMenu_Top.SetActive(true);
        Crosshair.SetActive(false);
        //Crosshair.SetActive(false);
        //P_MM.SetActive(true); P_Quit.SetActive(true);
        //P_Options.SetActive(true); P_Retry.SetActive(true);
    }

    private void _pauseUI_Deactive()
    {
        EscapeMenu_Top.SetActive(false); EscapeMenu_Options.SetActive(false); EscapeMenu_Controls.SetActive(false);
        Crosshair.SetActive(true);
        //P_MM.SetActive(false); P_Quit.SetActive(false); P_Options.SetActive(false);
        //P_Back.SetActive(false); SliderHolder.SetActive(false); P_Retry.SetActive(false);
        //Options_ControlsBack.SetActive(false); Options_Controls.SetActive(false); Options_ControlsDisplay.SetActive(false);
        //Crosshair.SetActive(true);
    }

    // Reloads Active Scene
    public void OnRetryPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnMainMenuButton()
    {
        GameMasterScript.SetMainMenuTrue();
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

    // References GameMaster variables
    private void _checkCollected()
    {
        if (GameMasterScript.Dash)
        {
            DashUI.SetActive(true);
        }
        else
        {
            DashUI.SetActive(false);
        }

        if (GameMasterScript.SoulOverflow)
        {
            SoulOverflowUI.SetActive(true);
        }
        else
        {
            SoulOverflowUI.SetActive(false);
        }

        if (GameMasterScript.Ashes)
        {
            AshesUI.SetActive(true);
        }
        else
        {
            AshesUI.SetActive(false);
        }
    }
}
