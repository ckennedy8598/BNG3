using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_Menu : MonoBehaviour
{
    public bool Paused;

    public GameObject P_MM;
    public GameObject P_Quit;
    public GameObject Crosshair;
    public void Start()
    {
        Paused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (GetIsPaused() == false))
        {
            Paused = true;
            Debug.Log("Sate of IsPaused: " + GetIsPaused());
            Time.timeScale = 0f;
            P_MM.SetActive(true); P_Quit.SetActive(true); Crosshair.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && (GetIsPaused() == true))
        {
            Paused = false;
            Debug.Log("Sate of IsPaused: " + GetIsPaused());
            Time.timeScale = 1.0f;
            P_MM.SetActive(false); P_Quit.SetActive(false); Crosshair.SetActive(true);
        }
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
