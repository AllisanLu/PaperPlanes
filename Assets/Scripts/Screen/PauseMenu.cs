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
                Pause();
            } else {
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
        MusicManager._instance.StopPauseMenuMusic();
        MusicManager._instance.UnPauseLevelMusic();
        MusicManager._instance.UnPauseLevel2Music();

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

    void Pause () {

        MusicManager._instance.PauseLevelMusic();
        MusicManager._instance.PauseLevel2Music();

        MusicManager._instance.PlayPauseMenuMusic();
        Debug.Log("Pause");

        Show();
        Time.timeScale = 0f;

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
