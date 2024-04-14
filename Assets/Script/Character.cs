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

public class Character : MonoBehaviour
{
    public List<string> CharacterOrder; 
    public GameObject CharacterTable; 
    public float BurgerScaleOnTable;
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
        gameObject.SetActive(true);
        await gameObject.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo);

        await CharacterMakeOrder();
    }
    private async UniTask CharacterMakeOrder()
    {
        textBubble.SetActive(true);
        await textBubble.transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo);
        await UniTask.Delay(500);
        await text.StartTyping();
        await UniTask.Delay(GameManager.Instance.DelayBeforeBoardUpdated);
        
        await GameManager.Instance.IngredientBoard.UpdateBoardAsync(CharacterOrder);
        await UniTask.Delay(GameManager.Instance.DelayAfterSpeech);
        
        await textBubble.transform.DOScale(0.8f, 0.2f);
        textBubble.SetActive(false);
        
        await UniTask.Delay(GameManager.Instance.DelayBeforeMoveToTable);
        await CharacterMoveToTable();
        //todo: char goes to their table
    }

    private async UniTask CharacterMoveToTable()
    {
        await gameObject.transform.DOScale(0.8f, 0.2f);
        gameObject.SetActive(false);
        characterAtTable.SetActive(true);
    }
    
}
