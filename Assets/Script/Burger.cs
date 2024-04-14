﻿using System;
using System.Collections.Generic;
using Script.Scroll;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Script
{
    public class Burger: MonoBehaviour, IDragHandler, IEndDragHandler
    { 
        [SerializeField] private RectTransform trashZone;

        public List<string> correctIngredients { get; set; }
        public List<string> currentIngredients { get; set; } = new();
            
        private RectTransform draggableObjectRectTransform;
        private Vector2 originalPosition;

        [SerializeField] private int maxIngredientCount;
        private int currentIngredientCount;

        private Character currentChar;
        private void Start()
        {
            draggableObjectRectTransform = gameObject.GetComponent<RectTransform>();
            originalPosition = draggableObjectRectTransform.anchoredPosition;

            currentChar = GameManager.Instance.CurrentCharacter;
            correctIngredients = currentChar.CharacterOrder;
        }

        public void AddIngredient(GameObject ingredientObject)
        {
            if (currentIngredientCount < maxIngredientCount)
            {
                //todo: check if FIRST/LAST ingredient is bread
                
                ingredientObject.transform.SetParent(gameObject.transform);
                var ingredient = ingredientObject.GetComponent<Ingredient>();
                currentIngredients.Add(ingredient.Name);
            
                ingredient.enabled = false;
                currentIngredientCount++;
                CheckIngredients();
            }
            else
            {
                //todo: message or animation of that you can't add more ingredients
                Debug.Log("Max ingredients reached");
            }
        }

        private void CheckIngredients()
        {
            if (currentIngredientCount == maxIngredientCount)
            {
                //todo: pentagram activation and send burger
                GameManager.Instance.Pentagram.IsActive = true;
            }
        }

        public bool CorrectAssembly()
        {
            for (int i = 0; i < correctIngredients.Count; i++)
            {
                if (currentIngredients[i] != correctIngredients[i])
                    return false;
            }
            return true;
        }
        
        public void OnBurgerDestroy()
        {
            currentIngredients.Clear();
            currentIngredientCount = 0;
            
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
            draggableObjectRectTransform.anchoredPosition = originalPosition;
        }
        public virtual void OnDrag(PointerEventData eventData)
        {
            draggableObjectRectTransform.anchoredPosition += eventData.delta;
            GameManager.Instance.Pentagram.IsActive = false;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if (OverlapsWithZone(trashZone))
            {
                OnBurgerDestroy();
                return;
            }
            
            draggableObjectRectTransform.anchoredPosition = originalPosition;
            if (currentIngredientCount == maxIngredientCount)
            {
                GameManager.Instance.Pentagram.IsActive = true;
            }
        }
        
        
        private bool OverlapsWithZone(RectTransform zoneRect)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(draggableObjectRectTransform, zoneRect.position) 
                || RectTransformUtility.RectangleContainsScreenPoint(zoneRect, draggableObjectRectTransform.position))
            {
                return true;
            }

            return false;
        }
    }
}