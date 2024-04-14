using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Script;
using Script.Scroll;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject textBubble;
    [SerializeField] private TypeWriterText text;

    [SerializeField] private List<IngredientName> characterOrder;

    private void Start()
    {
        textBubble.SetActive(false);
        CharacterAppear().Forget();
    }

    public async UniTask CharacterAppear()
    {
        await CharacterAppearAsync();

        await SpeechStart();
        await GameManager.Instance.IngredientBoard.UpdateBoardAsync(characterOrder);
        await UniTask.Delay(1500);
        await SpeechEnd();
    }

    private async UniTask CharacterAppearAsync()
    {
        await gameObject.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo);
    }
    private async UniTask SpeechStart()
    {
        textBubble.SetActive(true);
        await textBubble.transform.DOScale(1.2f, 0.1f).SetLoops(2, LoopType.Yoyo);
        await text.StartTyping();
        await UniTask.Delay(3000);
    }

    private async UniTask SpeechEnd()
    {
        await textBubble.transform.DOScale(0.9f, 0.1f);
        textBubble.SetActive(false);
        //todo: char goes to their table
        
    }
}
