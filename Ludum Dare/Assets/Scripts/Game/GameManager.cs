using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool takingAway = false;
    [SerializeField] private Image timerImage;
    [SerializeField] private Text loopsText;
    [SerializeField] private Character character;

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
        loopsText.text = loopsRemaining + "/" + nbOfTimeLoopsMax;
    }

    private void Update()
    {
        if (timer < timeMax)
        {
            UpdateTimer();
        }
        else if (timer >= timeMax)
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

        timerImage.fillAmount = timer / timeMax;
    }

    private void EndTimeLoop()
    {
        if (loopsRemaining < nbOfTimeLoopsMax)
        {
            loopsRemaining++;
            loopsText.text = loopsRemaining + "/" + nbOfTimeLoopsMax;
            //character.ResetLoop();
            timer = 0;
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
