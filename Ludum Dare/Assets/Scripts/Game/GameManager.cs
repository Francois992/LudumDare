using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

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

    [SerializeField] private bool isLastLevel = false;

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
        if (timer < timeMax && !isFading)
        {
            UpdateTimer();
        }
        else if (timer >= timeMax && !isFading)
        {
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
            isFading = true;

            SoundManager.instance.PlayTimeDistortion();

            UIManager.instance.TimeFadeIn(.5f, () =>
            {
                Character.instance.RestartLoop(() =>
                {
                    UIManager.instance.TimeFadeOut(.5f, () =>
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

    public void FinishedLevel()
    {
        if (!isLastLevel)
        {
            UIManager.instance.LevelFadeIn(1.5f, () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }
        else
        {
            UIManager.instance.TimeFadeIn(1.5f, () =>
            {
                SceneManager.LoadScene("MainMenu");
            });

        }
    }

    public void ReloadLevel()
    {
        UIManager.instance.LevelFadeIn(1.5f, () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    private void GameOver()
    {
        throw new NotImplementedException();
    }
}
