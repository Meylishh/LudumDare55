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
        [SerializeField] private RectTransform trashZone;

        public List<string> correctIngredients { get; set; }
        public List<string> currentIngredients { get; set; } = new();
            
        private RectTransform draggableObjectRectTransform;
        private Vector2 originalPosition;

        [SerializeField] private int maxIngredientCount;
        private int currentIngredientCount;

        private Character currentChar;
        public bool AssembleFull { get; private set; }
        private void Start()
        {
            GameManager.OnOrderFinished += _ => Reset();
            
            draggableObjectRectTransform = gameObject.GetComponent<RectTransform>();
            originalPosition = draggableObjectRectTransform.anchoredPosition;

            currentChar = GameManager.Instance.CurrentCharacter;
            correctIngredients = currentChar.CharacterOrder;
        }

        public void AddIngredient(GameObject ingredientObject)
        {
            if (currentIngredientCount < maxIngredientCount)
            {
                ingredientObject.transform.SetParent(gameObject.transform);
                var ingredient = ingredientObject.GetComponent<Ingredient>();
                currentIngredients.Add(ingredient.Name);
            
                ingredient.enabled = false;
                currentIngredientCount++;
                CheckIngredients();
            }
            else
            {
                AssembleFull = true;
                Debug.Log("Max ingredients reached");
            }
        }

        private void CheckIngredients()
        {
            if (currentIngredientCount == maxIngredientCount)
            {
                GameManager.Instance.Pentagram.IsActive = true;
            }
        }

        public bool CorrectAssembly()
        {
            //todo: bring back normal ingredient order?
            //currentIngredients.Reverse();
            for (int i = 0; i < correctIngredients.Count; i++)
            {
                if (currentIngredients[i] != correctIngredients[i])
                {
                    GameManager.Instance.BadEnd = true;
                    return false;
                }
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
            AssembleFull = false;
        }
        public virtual void OnDrag(PointerEventData eventData)
        {
            Vector2 sensitivity = new Vector2(Screen.width / GameManager.Instance.ReferenceWidth, Screen.height / GameManager.Instance.ReferenceHeight);
            draggableObjectRectTransform.anchoredPosition += eventData.delta * sensitivity;

            GameManager.Instance.Pentagram.IsActive = false;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if (OverlapsWithZone(trashZone))
            {
                OnBurgerDestroy();
                AudioManager.Instance.PlaySFX("FireTrash");
                return;
            }
            
            draggableObjectRectTransform.anchoredPosition = originalPosition;
            if (currentIngredientCount == maxIngredientCount)
            {
                GameManager.Instance.Pentagram.IsActive = true;
            }
        }

        private void Reset()
        {
            currentIngredients.Clear();
            currentIngredientCount = 0;
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