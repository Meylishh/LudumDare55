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
    [SerializeField] private float slideDuration;
    private GameObject cameraObject;
    private float slideOriginalPosition;
    private bool workspaceActive;
    private void Start()
    {
        cameraObject = Camera.main.gameObject;
        slideOriginalPosition = cameraObject.transform.position.y;
        changeScreenButton.onClick.AddListener(SlideScreen);
    }

    private void SlideScreen()
    {
        SlideScreenAsync().Forget();
    }
    private async UniTask SlideScreenAsync()
    {
        if (!workspaceActive)
        {
            changeScreenButton.interactable = false;
            await cameraObject.transform.DOMoveY(-slideDownAmount, slideDuration);
            changeScreenButton.interactable = true;
            workspaceActive = true;
        }
        else
        {
            changeScreenButton.interactable = false;
            await cameraObject.transform.DOMoveY(slideOriginalPosition, slideDuration);
            changeScreenButton.interactable = true;
            workspaceActive = false;
        }
    }
}
