using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    private bool isFading = false;

    public static GameManager instance;
    [Header("Timer")]
    public float timeMax = 30.0f;
    public float timer = 0f;

    [Header("Time Loops")]
    public int nbOfTimeLoopsMax = 15;
    private int loopsRemaining = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        UIManager.instance.UpdateLoopCounter(loopsRemaining, nbOfTimeLoopsMax);
    }

    private void Update()
    {
        if (timer < timeMax)
        {
            UpdateTimer();
        }
        else if (timer >= timeMax && !isFading)
        {
            isFading = true;
            EndTimeLoop();
        }
    }

    private void UpdateTimer()
    {
        if (timer >= timeMax)
        {
            timer = timeMax;
        }

        timer += Time.deltaTime;
        UIManager.instance.UpdateTimer(timer, timeMax);
    }

    public void EndTimeLoop()
    {
        if (loopsRemaining < nbOfTimeLoopsMax)
        {
            UIManager.instance.FadeIn(.5f, () =>
            {
                Character.instance.RestartLoop(() =>
                {
                    UIManager.instance.FadeOut(.5f, () =>
                    {
                        loopsRemaining++;
                        timer = 0;
                        UIManager.instance.UpdateLoopCounter(loopsRemaining, nbOfTimeLoopsMax);
                        isFading = false;
                        Character.instance.isFreezing = false;
                    });
                });
            });
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        throw new NotImplementedException();
    }
}
