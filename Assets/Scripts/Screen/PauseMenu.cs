using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static FMOD.Studio.EventInstance Button;

    private bool GameIsPaused {
        get { return gameIsPaused; }
        set {
            if (value) {
                Pause();
            } else {
                Resume();
            }
            gameIsPaused = value;
        }
    }
    private static bool gameIsPaused;

    public GameObject pauseMenuUI, pauseButton;
    public CanvasGroup canvasGroup;
    // Update is called once per frame

    public static PauseMenu instance;

    public MusicManager music;

    

    private void Awake()
    {
        pauseButton.GetComponent<Button>().enabled = true;
        instance = this;    
    }
    void Start() {
        Button = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Button");
        canvasGroup = pauseMenuUI.GetComponent<CanvasGroup>();
        Hide();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           // Debug.Log("Esc Pressed!");
            if (GameIsPaused) {
                GameIsPaused = false;

                //Resume();

            } else {    
                GameIsPaused = true;

                //Pause();

            }
        }
    }

    // public void playButton() 
    // {
    //     Button.start();
    //     Button.release();
    // }

    public void Unpause()
    {

        pauseButton.GetComponent<Button>().enabled = true;
        GameIsPaused = false;
        Hide();
    }

    public void Resume () {

        pauseButton.GetComponent<Button>().enabled = true;
        MusicManager._instance.StopPauseMenuMusic();
        MusicManager._instance.UnPauseLevelMusic();
        MusicManager._instance.UnPauseLevel2Music();
        MusicManager._instance.UnPauseLevel3Music();


        Hide();
        Time.timeScale = 1f;
    }

    void Hide() {
     canvasGroup.alpha = 0f; //this makes everything transparent
     canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    void Show() {
     canvasGroup.alpha = 1f;
     canvasGroup.blocksRaycasts = true;
    }

    public void Pause () {
        pauseButton.GetComponent<Button>().enabled = false;
        MusicManager._instance.PauseLevelMusic();
        MusicManager._instance.PauseLevel2Music();
        MusicManager._instance.PauseLevel3Music();

        MusicManager._instance.PlayPauseMenuMusic();
        Debug.Log("Pause");

        Show();
        Time.timeScale = 0f;

    }


    public void LoadMenu() {
        Unpause();
        Debug.Log("Loading Menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public bool isPaused()
    {
        return GameIsPaused;
    }

    public void setPaused() {
        GameIsPaused = true;
    }

}
