using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Script.Scroll;
using Unity.VisualScripting;

namespace Script
{
    public class GameLoopManager
    {
        public async UniTask StartSession()
        {
            await UniTask.Delay(GameManager.Instance.DelayBetweenChars);
            await GameManager.Instance.CurrentCharacter.CharacterAppearAsync();
        }

        public async UniTask FinishSession()
        {
            await GameManager.Instance.Pentagram.SendBurgerAsync(GameManager.Instance.CurrentCharacter);
            await GameManager.Instance.IngredientBoard.ClearBoardAsync();
            
            GameManager.Instance.SwitchCharacter();
            GameManager.Instance.Burger.correctIngredients = GameManager.Instance.CurrentCharacter.CharacterOrder;
            GameManager.Instance.Burger.currentIngredients.Clear();

            GameManager.Instance.Pentagram.IsActive = false;
        }
    }
}