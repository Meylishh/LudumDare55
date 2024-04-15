using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject tutorialScreen;
    
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button proceedButton;

    private void Start()
    {
        tutorialScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
        
        startGameButton.onClick.AddListener(OnStartButtonPress);
        proceedButton.onClick.AddListener(OnProceedButtonPress);
    }

    private void OnStartButtonPress()
    {
        startGameButton.interactable = false;
        mainMenuScreen.SetActive(false);
        tutorialScreen.SetActive(true);
    }

    private void OnProceedButtonPress()
    {
        proceedButton.interactable = false;
        SceneManager.LoadScene(1);
    }
}
