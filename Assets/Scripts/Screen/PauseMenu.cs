using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public CanvasGroup canvasGroup;
    // Update is called once per frame

    public static PauseMenu instance;

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
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume () {
        Hide();
        Time.timeScale = 1f;
        GameIsPaused = false;
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
        Show();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


    public void LoadMenu() {
        Debug.Log("Loading Menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public bool isPaused()
    {
        return GameIsPaused;
    }
}
