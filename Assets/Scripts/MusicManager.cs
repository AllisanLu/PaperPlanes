using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //SceneManager.sceneLoaded += OnSceneLoaded
    }

    void Update()
    {
        LevelSong = FMODUnity.RuntimeManager.CreateInstance(reference);

        if (currentScene != UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) {
            levelChanged = true;
            currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
        if (levelChanged) {
            StartLevelMusic();
            reference = reference;
            LevelSong = FMODUnity.RuntimeManager.CreateInstance(reference);
            Debug.Log(currentScene);
            Debug.Log(reference);


            levelChanged = false;
        }
        // if (!levelStarted && currentScene != "MainMenu") {
        //     StartLevelMusic();
        //     levelStarted = true;
        //    // Debug.Log(currentScene);
        // }
        //Debug.Log(currentScene);


        // if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu") {
        //     // LevelSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //     // LevelSong.release();
        //     Debug.Log("title song started");
        // } else {
        //     // TitleSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //     // TitleSong.release();
        //     Debug.Log("title song stopped");
        //     Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        // }
        // if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu") {
        //     Debug.Log("MainMenu");
        // }
        if (currentScene != "MainMenu") {
            StopTitleMusic();
            //StartLevelMusic();
            Debug.Log(reference);
        }


    }

    //NONE OF THE BELOW FUNCTIONS ACTUALLY WORK

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
        LevelSong.release();
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
        // if(PauseMenu.instance.isPaused()) {
        //     //_instance.PlayPauseMenuMusic();
        //     //_instance.PlayPauseMenuMusic();
        //     Debug.Log("Paused: " + PauseMenu.instance.isPaused());
        //     Level1Song.setPaused(true);

        //     if (pauseSongStarted == false) {
        //         //PauseSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //         pauseSongStarted = true;

        //         PauseSong.start();
        //         Debug.Log("Song started:" + pauseSongStarted);

        //     }
        //     //PauseSong.start();


        //     //PlayPauseMenuMusic();
        // }
        //Debug.Log("hi");
    }

    public void StopPauseMenuMusic() {

        PauseSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //PauseSong.release();
        // if(!PauseMenu.instance.isPaused()) {
        //     Level1Song.setPaused(false);    
        //     Debug.Log("Paused: " + PauseMenu.instance.isPaused());

        //     if (pauseSongStarted == true) {
        //         //Level1Song.start();
        //         pauseSongStarted = false;           

        //         PauseSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //         PauseSong.release();
        //         Debug.Log("Song started:" + pauseSongStarted);


        //     }
        //     //_instance.StopPauseMenuMusic();
        //     //PauseSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //     Debug.Log("Bye");

        // }
        //Debug.Log("hi");
    }
}
