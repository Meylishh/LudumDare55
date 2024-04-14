﻿using System;
using Script.Scroll;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class Pentagram: MonoBehaviour
    {
        [SerializeField] private Color inactiveColor;
        [SerializeField] private Color activeColor;
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
             image.color = inactiveColor;
             sendBurgerButton.onClick.AddListener(OnSendBurgerButtonPress);
         }

         private void OnSendBurgerButtonPress()
         {
             if (IngredientBank.Instance.Burger.CorrectAssembly())
             {
                 Debug.Log("Correct burger :D");
             }
             else
             {
                 Debug.Log("Incorrect :(");
             }
         }
         private void ActivatePentagram()
        {
            image.color = activeColor;
            //todo: button activation animation
            sendBurgerButton.interactable = true;
        }

        private void DisablePentagram()
        {
            image.color = inactiveColor;
            sendBurgerButton.interactable = false;
        }
    }
}