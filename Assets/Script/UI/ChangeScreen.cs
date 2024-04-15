using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScreen : MonoBehaviour
{
    [SerializeField] private Button changeScreenButton;
    [SerializeField] private float slideDownAmount;
    [SerializeField] private float slideUpAmount;
    [SerializeField] private float slideDuration;
    [SerializeField] private GameObject screensObject;
    public bool workspaceActive;
    
    //todo: slide up when a new customer comes in (non-interactive screen during?) 
    private void Start()
    {
        changeScreenButton.onClick.AddListener(SlideScreen);
    }

    public void SlideScreen()
    {
        SlideScreenAsync().Forget();
    }
    private async UniTask SlideScreenAsync()
    {
        if (!workspaceActive)
        {
            changeScreenButton.interactable = false;
            await screensObject.transform.DOMoveY(slideDownAmount, slideDuration);
            changeScreenButton.interactable = true;
            workspaceActive = true;
        }
        else
        {
            changeScreenButton.interactable = false;
            await screensObject.transform.DOMoveY(slideUpAmount, slideDuration);
            changeScreenButton.interactable = true;
            workspaceActive = false;
        }
    }
}
