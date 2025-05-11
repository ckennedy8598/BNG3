using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Main : MonoBehaviour
{
    public GameObject GameMaster;
    public GameMaster gm_script;

    public GameObject Buttons;
    public GameObject Title;
    public GameObject Jukebox;
    public Animator TitleAnim;
    public Animator FireAnim;

    public AudioSource ButtonPress;

    private void Start()
    {
        GameMaster = GameObject.Find("Game Master");
        gm_script = GameMaster.GetComponent<GameMaster>();
        gm_script.MainMenu = true;

        FireAnim = GameObject.Find("FireAnim").GetComponent<Animator>();
        Title = GameObject.Find("Title");
        TitleAnim = Title.GetComponent<Animator>();

        Jukebox = GameObject.Find("Jukebox");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void OnPlayButtion()
    {
        gm_script.SetNewGameSettings();
        _setButtonsInactive();
        FireAnim.SetTrigger("PlayButtonClick");
        TitleAnim.SetTrigger("PlayButtonClick");
        ButtonPress.Play();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("New Game!");
    }

    public void OnCreditsButton()
    {
        SceneManager.LoadScene(8);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    private void _setButtonsInactive()
    {
        Buttons.SetActive(false);
        Jukebox.SetActive(false);
    }
}
