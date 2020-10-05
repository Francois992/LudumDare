using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource titleTheme;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource pressurePlate;
    [SerializeField] private AudioSource timeDistortion;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource timePassing;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    //--------------------------- Play Music ---------------------------------//

    public void PlayFootstep()
    {
        footstep.Play();
    }
    public void PlayTitleTheme()
    {
        titleTheme.Play();
    }
    public void PlayJump()
    {
        jump.Play();
    }
    public void PlayPressurePlate()
    {
        pressurePlate.Play();
    }
    public void PlayTimeDistortion()
    {
        timeDistortion.Play();
    }
    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }
    public void PlayTimePassing()
    {
        timePassing.Play();
    }

    //--------------------- Stop Music ----------------------------------//

    public void StopFootstep()
    {
        footstep.Stop();
    }
    public void StopTitleTheme()
    {
        titleTheme.Stop();
    }
    public void StopJump()
    {
        jump.Stop();
    }
    public void StopPressurePlate()
    {
        pressurePlate.Stop();
    }
    public void StopTimeDistortion()
    {
        timeDistortion.Stop();
    }
    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
    public void StopTimePassing()
    {
        timePassing.Stop();
    }

    //------------------------- bool isPlaying---------------------------------------//

    public bool isFootstepPlaying()
    {
        return footstep.isPlaying;
    }
}
