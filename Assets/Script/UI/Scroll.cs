using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Script.Scroll;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    [Header("Scroll")]
    [SerializeField] private GameObject scrollObject;
    [SerializeField] private Button scrollButton;
    [SerializeField] private RectTransform scrollZone;
    [SerializeField] private float scrollOpenDistance;
    [SerializeField] private float scrollCloseDistance;
    [SerializeField] private float scrollOpenTime;

    [Header("RuneSpawn")] 
    [SerializeField] private Button runeButton;
    [SerializeField] private GameObject ingredientToSpawnPrefab;
    
    private bool isOpened;

    private void Start()
    {
        scrollButton.onClick.AddListener(OnClickScroll);
        runeButton.onClick.AddListener(OnClickRune);
        runeButton.interactable = false;
    }

    private void OnClickScroll()
    {
        OnClickScrollAsync().Forget();
    }
    
    private async UniTask OnClickScrollAsync()
    {
        if (!isOpened)
        {
            scrollButton.interactable = false;
            await scrollZone.transform.DOScaleY(-scrollOpenDistance, scrollOpenTime);
            scrollButton.interactable = true;
            isOpened = true;
            runeButton.interactable = true;
        }
        else
        {
            scrollButton.interactable = false;
            await scrollZone.transform.DOScaleY(scrollCloseDistance, scrollOpenTime);
            scrollButton.interactable = true;
            isOpened = false;
            runeButton.interactable = false;
        }
    }

    private void OnClickRune()
    {
        OnClickRuneAsync().Forget();
    }
    private async UniTask OnClickRuneAsync()
    {
        GameManager.Instance.InstantiateIngredient(ingredientToSpawnPrefab);
    }
    
    
    
}
