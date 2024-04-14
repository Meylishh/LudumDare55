using System;
using System.Collections.Generic;
using Script.Scroll;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Script
{
    public class Burger: MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private List<Ingredient> correctIngredients;
        private List<Ingredient> currentIngredients = new();
            
        private RectTransform draggableObjectRectTransform;
        private Vector2 originalPosition;

        [SerializeField] private int maxIngredientCount;
        private int currentIngredientCount;

        public UnityAction OnIngredientAdded;
        
        private void Start()
        {
            draggableObjectRectTransform = gameObject.GetComponent<RectTransform>();
            originalPosition = draggableObjectRectTransform.anchoredPosition;

            OnIngredientAdded += CheckIngredients;
        }

        public void AddIngredient(GameObject ingredientObject)
        {
            ingredientObject.transform.SetParent(gameObject.transform);
            var ingredient = ingredientObject.GetComponent<Ingredient>();
            currentIngredients.Add(ingredient);
            
            ingredient.enabled = false;
            currentIngredientCount++;
            OnIngredientAdded.Invoke();
        }

        private void CheckIngredients()
        {
            if (currentIngredientCount == maxIngredientCount)
            {
                //todo: pentagram activation and send burger
                if (CorrectAssembly())
                {
                    //todo: happy customer
                }
            }
        }

        private bool CorrectAssembly()
        {
            for (int i = 0; i < correctIngredients.Count; i++)
            {
                if (correctIngredients[i] != currentIngredients[i])
                    return false;
            }
            return true;
        }

        private void OnBurgerDestroy()
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
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            //todo: Check if overlaps with trash bin 
            draggableObjectRectTransform.anchoredPosition = originalPosition;
        }
        
    }
}