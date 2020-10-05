using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Image timerImage;
    [SerializeField] private Image timerFadeImage;
    [SerializeField] private Image startLevelFadeImage;
    [SerializeField] private Text loopsText;




    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        startLevelFadeImage.DOFade(1f, 0f);
        startLevelFadeImage.DOFade(0f, 1.5f);
        TimeFadeOut(0f);
    }

    public void UpdateTimer(float timer, float timeMax)
    {
        timerImage.fillAmount = timer / timeMax;
    }

    public void UpdateLoopCounter(int loopsRemaining, int nbOfTimeLoopsMax)
    {
        loopsText.text = loopsRemaining + "/" + nbOfTimeLoopsMax;
    }

    public void TimeFadeIn(float duration ,TweenCallback tweenCallback = null)
    {
        timerFadeImage.DOFade(1f, duration)
            .OnComplete(tweenCallback);
    }

    public void TimeFadeOut(float duration, TweenCallback tweenCallback = null)
    {
        timerFadeImage.DOFade(0f, duration)
            .OnComplete(tweenCallback);
    }

    public void LevelFadeIn(float duration, TweenCallback tweenCallback = null)
    {
        startLevelFadeImage.DOFade(1f, duration)
            .OnComplete(tweenCallback);
    }

    public void LevelFadeOut(float duration, TweenCallback tweenCallback = null)
    {
        startLevelFadeImage.DOFade(0f, duration)
            .OnComplete(tweenCallback);
    }
}
