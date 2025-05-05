using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Main : MonoBehaviour
{
    public GameObject GameMaster;
    public GameMaster gm_script;

    private void Start()
    {
        GameMaster = GameObject.Find("Game Master");
        gm_script = GameMaster.GetComponent<GameMaster>();
        gm_script.MainMenu = true;


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void OnPlayButtion()
    {
        gm_script.SetNewGameSettings();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("New Game!");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
