using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Script;
using Script.Scroll;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public List<string> CharacterOrder; 
    public GameObject CharacterTable; 
    public float BurgerScaleOnTable;
    public bool CharacterAppearing { get; private set; }
    [SerializeField] private GameObject characterAtTable;
    [SerializeField] private GameObject textBubble;
    [SerializeField] private TypeWriterText text;
    
    private void Start()
    {
        textBubble.SetActive(false);
        gameObject.SetActive(false);
        characterAtTable.SetActive(false);
    }

    public async UniTask CharacterAppearAsync()
    {
        CharacterAppearing = true;
        
        AudioManager.Instance.PlaySFX("FootSteps");
        if (GameManager.Instance.ChangeScreen.workspaceActive)
        {
            GameManager.Instance.ChangeScreen.SlideScreen();
        }
        
        gameObject.SetActive(true);

        var image = gameObject.GetComponent<Image>();
        await image.DOFade(1, 0.2f).ToUniTask();
        
        await CharacterMakeOrder();
    }
    private async UniTask CharacterMakeOrder()
    {
        textBubble.SetActive(true);
        await textBubble.transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo);
        await UniTask.Delay(100);
        await text.StartTyping();
        await UniTask.Delay(GameManager.Instance.DelayBeforeBoardUpdated);
        
        await GameManager.Instance.IngredientBoard.UpdateBoardAsync(CharacterOrder);
        await UniTask.Delay(GameManager.Instance.DelayAfterSpeech);
        
        await textBubble.transform.DOScale(0.8f, 0.2f);
        textBubble.SetActive(false);
        
        await UniTask.Delay(GameManager.Instance.DelayBeforeMoveToTable);
        await CharacterMoveToTable();
    }

    private async UniTask CharacterMoveToTable()
    {
        var image = gameObject.GetComponent<Image>();
        await image.DOFade(0, 0.2f).ToUniTask();
        gameObject.SetActive(false);
        
        AudioManager.Instance.PlaySFX("FootSteps");
        characterAtTable.SetActive(true);
        
        CharacterAppearing = false;
    }
    
}
