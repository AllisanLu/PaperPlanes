using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public static FMOD.Studio.EventInstance PauseSong;
    public static FMOD.Studio.EventInstance Level1Song;
    //public PauseMenu pausemenu;


    FMOD.Studio.Bus MasterBus;
    // Start is called before the first frame update
    void Start()
    {
        PauseSong = FMODUnity.RuntimeManager.CreateInstance("event:/Pause");
        Level1Song = FMODUnity.RuntimeManager.CreateInstance("event:/Level_1");
    }

    void Update()
    {

    }

    //NONE OF THE BELOW FUNCTIONS ACTUALLY WORK
    public void StartLevel1Music() {
        Level1Song.start();
    }

    public void StopLevel1Music() {
        Level1Song.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Level1Song.release();
    }

    public void PauseLevel1Music() {
        Level1Song.setPaused(true);

    }


    public void UnPauseLevel1Music() {
        Level1Song.setPaused(false);
    }

    public void PlayPauseMenuMusic() {
        //PauseSong.start();
        Debug.Log("hi");
    }

    public void StopPauseMenuMusic() {
        //PauseSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //PauseSong.release();
        Debug.Log("hi");
    }
}
