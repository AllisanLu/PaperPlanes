using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MusicManager : MonoBehaviour
{
    public static MusicManager _instance;
    public static MusicManager Instance { get {return _instance; } }
    [SerializeField] public FMODUnity.EventReference reference1;
    [SerializeField] public FMODUnity.EventReference reference2;

    public static FMOD.Studio.EventInstance PauseSong;
    public static FMOD.Studio.EventInstance LevelSong;
    public static FMOD.Studio.EventInstance Level2Song;

    public static FMOD.Studio.EventInstance TitleSong;
    public static bool levelStarted = false;
    public static bool levelChanged = false;
    private string currentScene;
    private bool titleSongPlaying = false;
    private bool levelSongPlaying = false;
    private bool level2SongPlaying = false;

    //private int titleSongCount = 0;

    //public static bool pauseSongStarted = false;

    //public PauseMenu pausemenu;
    FMOD.Studio.Bus MasterBus;
    
    // Start is called before the first frame update
    void Start()
    {
        PauseSong = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/Pause/PauseSong");
        LevelSong = FMODUnity.RuntimeManager.CreateInstance(reference1);
        Level2Song = FMODUnity.RuntimeManager.CreateInstance(reference2);

        TitleSong = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/Title2");
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        //TitleSong.start();
        StartTitleMusic();
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
            if (currentScene != "MainMenu") {
                StopTitleMusic();
                titleSongPlaying = false;
                //StartLevelMusic();
            }
            if (!(currentScene == "Tutorial" || currentScene == "L1A2" || currentScene == "L1A3" ) && levelSongPlaying) {
                StopLevelMusic();
                levelSongPlaying = false;
                //StartLevelMusic();
            }
            if (currentScene != "L2" && level2SongPlaying) {
                StopLevel2Music();
                level2SongPlaying = false;
            }
            if (!titleSongPlaying && currentScene == "MainMenu") {
                StartTitleMusic();
                titleSongPlaying = true;
            }
            //LevelSong = FMODUnity.RuntimeManager.CreateInstance(reference);
            if (!levelSongPlaying && currentScene == "Tutorial") {
                StartLevelMusic();
                //StartLevel2Music();

                levelSongPlaying = true;
                Debug.Log("reached");
            }

            if (!level2SongPlaying && currentScene == "L2") {
                StartLevel2Music();
                level2SongPlaying = true;
               

            }

            if (currentScene == "MainMenu") {
                StopPauseMenuMusic();
            }



            levelChanged = false;
        }

        // if (currentScene != "MainMenu") {
        //     StopTitleMusic();
        //     titleSongPlaying = false;
        //     //StartLevelMusic();
        // }
        // if (!(currentScene == "Tutorial" || /*currentScene == "L1A2" ||*/ currentScene == "L1A3" ) && levelSongPlaying) {
        //     StopLevelMusic();
        //     levelSongPlaying = false;
        //     //StartLevelMusic();
        // }
        // if (currentScene != "L12A" && level2SongPlaying) {
        //     StopLevel2Music();
        //     level2SongPlaying = false;
        // }


    }


    public void StartLevel2Music() {
        Level2Song.start();
    }

    public void PauseLevel2Music() {

        Level2Song.setPaused(true);
    }


    public void UnPauseLevel2Music() {
        Level2Song.setPaused(false);
    }

    public void StopLevel2Music() {
        Level2Song.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //LevelSong.release();
    }

    public void StartTitleMusic() {
        TitleSong.start();
    }

    public void StopTitleMusic() {
        TitleSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        TitleSong.release();
    }

    // public void ChangeLevelSong() {
    //     LevelSong = FMODUnity.RuntimeManager.CreateInstance(reference);
    // }

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
