using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Script;
using Script.Scroll;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    [Header("Scroll")]
    [SerializeField] private Button scrollButton;
    [SerializeField] private GameObject openedScroll;
    [SerializeField] private float openedScrollFinalHeight;
    [SerializeField] private float scrollOpenTime;

    [Header("RuneSpawn")] 
    [SerializeField] private Button runeButton;
    [SerializeField] private GameObject ingredientToSpawnPrefab;

    private RectTransform openedScrollRect;
    private float initialOpenedScrollHeight;
    private bool isOpened;

    private void Start()
    {
        openedScrollRect = openedScroll.GetComponent<RectTransform>();
        initialOpenedScrollHeight = openedScrollRect.rect.height;
        
        openedScroll.SetActive(false);
        
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
            openedScroll.SetActive(true);
            await openedScrollRect.DOSizeDelta(new Vector2(openedScrollRect.rect.width, openedScrollFinalHeight), scrollOpenTime).SetEase(Ease.InOutSine);
            
            scrollButton.interactable = true;
            isOpened = true;
            runeButton.interactable = true;
        }
        else
        {
            scrollButton.interactable = false;
            await openedScrollRect.DOSizeDelta(new Vector2(openedScrollRect.rect.width, initialOpenedScrollHeight), scrollOpenTime).SetEase(Ease.InOutSine);
            openedScroll.SetActive(false);
            
            scrollButton.interactable = true;
            isOpened = false;
            runeButton.interactable = false;
        }
    }

    private void OnClickRune()
    {
        AudioManager.Instance.PlaySFX("FoodSpawn");
        GameManager.Instance.InstantiateIngredient(ingredientToSpawnPrefab);
    }
 
}
