using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Script.Scroll;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class Pentagram: MonoBehaviour
    {
        [SerializeField] private Sprite inactiveImage;
        [SerializeField] private Sprite activeImage;
        [SerializeField] private Button sendBurgerButton;
        [SerializeField] private Image image;

        private bool isActive;
        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                if (isActive)
                {
                    ActivatePentagram();
                }
                else
                {
                    DisablePentagram();
                }
            }
        }

         private void Start()
         {
             GameManager.OnOrderFinished += SendBurger; 
             
             image.sprite = inactiveImage;
             sendBurgerButton.interactable = false;
             sendBurgerButton.onClick.AddListener(OnSendBurgerButtonPress);
         }

         private void OnSendBurgerButtonPress()
         {
             if (GameManager.Instance.Burger.CorrectAssembly())
             {
                 //todo: change sprite based on this
                 Debug.Log("Correct burger :D");
             }
             else
             {
                 //todo: FIX THAT SHIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIT
                 Debug.Log("Incorrect :(");
             }
             AudioManager.Instance.PlaySFX("TeleportToTable");
             
             GameManager.Instance.SwitchCharacter();
         }

         private void SendBurger(Character character)
         {
             SendBurgerAsync(character).Forget();
         }

         private async UniTask SendBurgerAsync(Character character)
         {
             if (GameManager.Instance.ChangeScreen.workspaceActive)
             {
                 GameManager.Instance.ChangeScreen.SlideScreen();
             }
             
             var burgerObj = GameManager.Instance.Burger.gameObject;
             
             await burgerObj.transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo);
             
             var burgerOnTable = Instantiate(burgerObj, character.CharacterTable.transform);
             burgerOnTable.GetComponent<Burger>().enabled = false;
             await burgerOnTable.transform.DOScale(character.BurgerScaleOnTable, 0f);
             
             GameManager.Instance.Burger.OnBurgerDestroy();
             isActive = false;
             DisablePentagram();
         }
         private void ActivatePentagram()
        {
            if (!GameManager.Instance.CurrentCharacter.CharacterAppearing)
            {
                image.sprite = activeImage;
                sendBurgerButton.interactable = true;
            }
        }

        private void DisablePentagram()
        {
            image.sprite = inactiveImage;
            sendBurgerButton.interactable = false;
        } 
    }
}