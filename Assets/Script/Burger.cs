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
        [SerializeField] private RectTransform trashZone;
        private List<Ingredient> currentIngredients = new();
            
        private RectTransform draggableObjectRectTransform;
        private Vector2 originalPosition;

        [SerializeField] private int maxIngredientCount;
        private int currentIngredientCount;
        
        
        private void Start()
        {
            draggableObjectRectTransform = gameObject.GetComponent<RectTransform>();
            originalPosition = draggableObjectRectTransform.anchoredPosition;
        }

        public void AddIngredient(GameObject ingredientObject)
        {
            if (currentIngredientCount < maxIngredientCount)
            {
                //todo: check if FIRST/LAST ingredient is bread
                
                ingredientObject.transform.SetParent(gameObject.transform);
                var ingredient = ingredientObject.GetComponent<Ingredient>();
                currentIngredients.Add(ingredient);
            
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
                IngredientBank.Instance.Pentagram.IsActive = true;
            }
        }

        public bool CorrectAssembly()
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
            IngredientBank.Instance.Pentagram.IsActive = false;
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
                IngredientBank.Instance.Pentagram.IsActive = true;
            }
        }
        
        
        private bool OverlapsWithZone(RectTransform zoneRect)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(draggableObjectRectTransform, zoneRect.position) 
                || RectTransformUtility.RectangleContainsScreenPoint(zoneRect, draggableObjectRectTransform.position))
            {
                return true;
            }

            Debug.Log("Images do not overlap.");
            return false;
        }
    }
}