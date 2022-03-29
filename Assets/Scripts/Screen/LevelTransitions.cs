using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelTransitions : MonoBehaviour
{
    public static LevelTransitions instance = null;
    public Animator transition;
    public float transitionTime = 1f;
    public TextMeshProUGUI levelTextObject;
    public List<string> levelText = new List<string>();

    private static int curr = 0;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        levelTextObject.text = levelText[curr];
    }

    public IEnumerator LevelTransition(string nextScene)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        curr++;
        levelTextObject.text = levelText[curr];
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(nextScene);
    }
}
