using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Scroll;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public IngredientName name;
    public string Name;
    private RectTransform draggableObjectRectTransform;
    private Vector2 lastFixedPosition;
    
    private void Start()
    {
        draggableObjectRectTransform = gameObject.GetComponent<RectTransform>();
        lastFixedPosition = draggableObjectRectTransform.anchoredPosition;
    }
  
    private void OnIngredientDestroy()
    {
        Destroy(gameObject);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 sensitivity = new Vector2(Screen.width / GameManager.Instance.ReferenceWidth, Screen.height / GameManager.Instance.ReferenceHeight);
        draggableObjectRectTransform.anchoredPosition += eventData.delta * sensitivity;
        //draggableObjectRectTransform.anchoredPosition += eventData.delta;
    }

    private void Update()
    {
        if (OverlapsWithZone(GameManager.Instance.TrashBinZone))
        {
            OnIngredientDestroy();
            AudioManager.Instance.PlaySFX("FireTrash");
            return;
        }

        if (OverlapsWithZone(GameManager.Instance.AssemblyZone) && !GameManager.Instance.Burger.AssembleFull)
        {
            GameManager.Instance.Burger.AddIngredient(gameObject);
            return;
        }
        
        if (OverlapsWithZone(GameManager.Instance.WorkspaceZone))
        {
            lastFixedPosition = draggableObjectRectTransform.anchoredPosition;
        }
        draggableObjectRectTransform.anchoredPosition = lastFixedPosition;
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


    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
