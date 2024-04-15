using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.Scroll;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class IngredientBoard: MonoBehaviour
    {
        [SerializeField] private List<TMP_Text> ingredientsTexts; 
        [SerializeField] private int letterDelay;

        private void Start()
        {
            ClearBoardAsync().Forget();

            GameManager.OnOrderFinished += _ => ClearBoard();
        }

        public async UniTask UpdateBoardAsync(List<string> ingredientNames)
        {
            for (int i = 0; i < ingredientNames.Count; i++)
            {
                await AddLine(ingredientsTexts[i], ingredientNames[i]);
            }
        }

        public void ClearBoard()
        {
            ClearBoardAsync().Forget();
        }
        public async UniTask ClearBoardAsync()
        {
            foreach (var text in ingredientsTexts)
            {
                UniTask.RunOnThreadPool(() => RemoveLine(text));
            }
            await UniTask.WhenAll();
        }
        private async UniTask RemoveLine(TMP_Text tmpText)
        {
            var originalText = tmpText.text;
            for (int i = originalText.Length; i >= 0; i--)
            {
                tmpText.text = originalText.Substring(0, i);
                await UniTask.Delay(letterDelay);
            }
        }
        private async UniTask AddLine(TMP_Text tmpText, string targetText)
        {
            string displayText;
            for (int i = 0; i <= targetText.Length; i++)
            {
                displayText = targetText.Substring(0, i);
                tmpText.text = displayText;
                await UniTask.Delay(letterDelay);
            }
        }
      
        
    }
}