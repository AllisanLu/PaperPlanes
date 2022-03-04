using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MusicManager : MonoBehaviour
{
    public static MusicManager _instance;
    public static MusicManager Instance { get {return _instance; } }
    [SerializeField] public FMODUnity.EventReference reference;
    public static FMOD.Studio.EventInstance PauseSong;
    public static FMOD.Studio.EventInstance LevelSong;
    public static FMOD.Studio.EventInstance TitleSong;
    public static bool levelStarted = false;
    public static bool levelChanged = false;
    private string currentScene;
    private bool levelSongPlaying;
    //private int titleSongCount = 0;

    //public static bool pauseSongStarted = false;

    //public PauseMenu pausemenu;
    FMOD.Studio.Bus MasterBus;
    
    // Start is called before the first frame update
    void Start()
    {
        PauseSong = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/Pause/PauseSong");
        LevelSong = FMODUnity.RuntimeManager.CreateInstance(reference);
        TitleSong = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/Title2");
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        TitleSong.start();
 
		DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        if(_instance != null && _instance != this) 
        {
            Destroy(gameObject);
        }
        else {
            _instance = this;
        }
    }

  

    void Update()
    {
        //LevelSong = FMODUnity.RuntimeManager.CreateInstance(reference);
        // SceneManager.activeSceneChanged = (prev, next) => {
        //     declare variable reference
        //     switch(next.name) {

        //     case "tutorial": reference = insert hard coded reference here; break;
        //     // ... rest of the thing
        //     }
        // // level song <- create new instance for the reference
        // }
        
        if (currentScene != UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) {
            levelChanged = true;
            currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }

        if (levelChanged) {

            //LevelSong = FMODUnity.RuntimeManager.CreateInstance(reference);
            if (!levelSongPlaying) {
                StartLevelMusic();
                levelSongPlaying = true;
            }



            levelChanged = false;
        }

        if (currentScene != "MainMenu") {
            StopTitleMusic();
            //StartLevelMusic();
        }


    }


    public void StopTitleMusic() {
        TitleSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        TitleSong.release();
    }

    public void ChangeLevelSong() {
        LevelSong = FMODUnity.RuntimeManager.CreateInstance(reference);
    }

    public void StartLevelMusic() {
        LevelSong.start();
    }

    public void StopLevelMusic() {
        LevelSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //LevelSong.release();
    }

    public void PauseLevelMusic() {

        LevelSong.setPaused(true);
        Debug.Log("LevelSong Paused");
    }


    public void UnPauseLevelMusic() {
        //LevelSong.setPaused(false);
        if (!LevelSong.isValid()){
            throw new UnityException("Song invalid unpause");
        }
        if (LevelSong.setPaused(true) != FMOD.RESULT.OK)
        {
            throw new UnityException("Cannot toggle pause");
        }
        LevelSong.setPaused(false);
        Debug.Log("LevelSong Unpaused");

    }

    public void PlayPauseMenuMusic() {
        PauseSong.start();
        Debug.Log("Song start");

    }

    public void StopPauseMenuMusic() {

        PauseSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

    }
}
