using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Image timerImage;
    [SerializeField] private Button testbutton;
    [SerializeField] private Image fadeImage;
    [SerializeField] private Image starFadeImage;
    [SerializeField] private Text loopsText;




    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        //starFadeImage.DOFade(1f, 0f);
        starFadeImage.DOFade(0f, 1.5f);
        FadeOut(0f);
    }

    // Update is called once per frame
    public void UpdateTimer(float timer, float timeMax)
    {
        timerImage.fillAmount = timer / timeMax;
    }

    public void UpdateLoopCounter(int loopsRemaining, int nbOfTimeLoopsMax)
    {
        loopsText.text = loopsRemaining + "/" + nbOfTimeLoopsMax;
    }

    public void FadeIn(float duration ,TweenCallback tweenCallback = null)
    {
        fadeImage.DOFade(1f, duration)
            .OnComplete(tweenCallback);
    }

    public void FadeOut(float duration, TweenCallback tweenCallback = null)
    {
        fadeImage.DOFade(0f, duration)
            .OnComplete(tweenCallback);
    }
}
