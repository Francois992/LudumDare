using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsQuitButton;
    [SerializeField] private GameObject optionsButton;


    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private Image startFadeInImage;
    [SerializeField] private EventSystem eventSystem;

    [SerializeField] private AudioSource menuMusic;

    private void Start()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        startFadeInImage.DOFade(0f, 0f);
    }

    public void StartGame()
    {
        startFadeInImage.DOFade(1f, 1.5f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(1);
            });

        float actualVolume = menuMusic.volume;
        DOVirtual.Float(actualVolume, 0f, 1.3f, UpdateMenuVolume);
    }

    void UpdateMenuVolume(float value)
    {
        menuMusic.volume = value;
    }

    public void OpenOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(optionsQuitButton);
    }

    public void QuitOptionsMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(optionsButton);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
