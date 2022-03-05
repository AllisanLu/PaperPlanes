using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool GameIsPaused {
        get { return gameIsPaused; }
        set {
            if (value) {
                // call pause
                Pause();
            } else {
                // call resume
                Resume();
            }
            gameIsPaused = value;
        }
    }
    private static bool gameIsPaused;

    public GameObject pauseMenuUI;
    public CanvasGroup canvasGroup;
    // Update is called once per frame

    public static PauseMenu instance;

    public MusicManager music;

    

    private void Awake()
    {
        instance = this;
    }
    void Start() {
        canvasGroup = pauseMenuUI.GetComponent<CanvasGroup>();
        Hide();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           // Debug.Log("Esc Pressed!");
            if (GameIsPaused) {
                //MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                GameIsPaused = false;

                //Resume();

            } else {    
                GameIsPaused = true;

                //Pause();

            }
        }
    }

    public void Unpause()
    {
        GameIsPaused = false;
        Hide();
    }

    public void Resume () {
        //MusicManager.PauseSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //MusicManager.PauseSong.release();
        //MusicManager.Level1Song.setPaused(false);
        // MusicManager.instance.StopPauseMenuMusic();
        MusicManager._instance.StopPauseMenuMusic();
        MusicManager._instance.UnPauseLevelMusic();
        Hide();
        Time.timeScale = 1f;

        //GameIsPaused = false;
    }

    void Hide() {
     canvasGroup.alpha = 0f; //this makes everything transparent
     canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    void Show() {
     canvasGroup.alpha = 1f;
     canvasGroup.blocksRaycasts = true;
    }

    void Pause () {
        //MusicManager.PauseSong.start();
        //MusicManager.instance.StopPauseMenuMusic();
        //MusicManager.LevelSong.setPaused(true);
        MusicManager._instance.PauseLevelMusic();
        //MusicManager._instance.StopLevelMusic();
        MusicManager._instance.PlayPauseMenuMusic();
        Debug.Log("Pause");

        Show();
        Time.timeScale = 0f;

        //GameIsPaused = true;
    }


    public void LoadMenu() {
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
