using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject firstTutorialScreen;
    [SerializeField] private GameObject secondTutorialScreen;
    
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button proceedButton;
    private bool proceedButtonPressed;
    
    private void Start()
    {
        firstTutorialScreen.SetActive(false);
        secondTutorialScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
        
        startGameButton.onClick.AddListener(OnStartButtonPress);
        proceedButton.onClick.AddListener(OnProceedButtonPress);
    }

    private void OnStartButtonPress()
    {
        startGameButton.interactable = false;
        mainMenuScreen.SetActive(false);
        firstTutorialScreen.SetActive(true);
    }

    private async void OnProceedButtonPress()
    {
        proceedButton.interactable = false;
        if (!proceedButtonPressed)
        {
            secondTutorialScreen.SetActive(true);
            proceedButtonPressed = true;
            await UniTask.Delay(300);
            proceedButton.interactable = true;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
