using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Script;
using UnityEngine;
using UnityEngine.UI;

public class Mixer : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button openButton;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private bool isOpened;
    private void Start()
    {
        gameObject.SetActive(false);
        
        openButton.onClick.AddListener(OnMixerOpen);
        closeButton.onClick.AddListener(OnMixerClose);
    }

    public void ToggleMusic()
    { 
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }
    private void OnMixerOpen()
    {
        if(!isOpened)
        {
            gameObject.SetActive(true);
        
            OnMixerOpenAsync().Forget();
            isOpened = true;
        }
    }
    private async UniTask OnMixerOpenAsync()
    {
        closeButton.interactable = false;
        openButton.interactable = false;
        await gameObject.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo);
        closeButton.interactable = true;
    }
    
    private void OnMixerClose()
    {
        if (isOpened)
        {
            OnMixerCloseAsync().Forget();
            isOpened = false;
        }
    }
    private async UniTask OnMixerCloseAsync()
    {
        closeButton.interactable = false;
        openButton.interactable = false;
        await gameObject.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo);
        gameObject.SetActive(false);
        openButton.interactable = true;
    }
}
