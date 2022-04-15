using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("OpeningCutscenes");
        CheckpointManager.resetPosition();
    }

    public void QuitGame() {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void Level1() {
        SceneManager.LoadScene("L1");
        CheckpointManager.resetPosition();
    }

    public void Level2() {
        SceneManager.LoadScene("L2");
        CheckpointManager.resetPosition();
    }

    public void Level3() {
        SceneManager.LoadScene("L3");
        CheckpointManager.resetPosition();
    }
}
