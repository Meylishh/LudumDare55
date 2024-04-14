using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Scroll;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ingredient : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public IngredientName name;
    private RectTransform draggableObjectRectTransform;
    private Vector2 lastFixedPosition;
    
    private void Start()
    {
        draggableObjectRectTransform = gameObject.GetComponent<RectTransform>();
        lastFixedPosition = draggableObjectRectTransform.anchoredPosition;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        draggableObjectRectTransform.anchoredPosition += eventData.delta;
    }

    private void OnIngredientDestroy()
    {
        Destroy(gameObject);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OverlapsWithZone(GameManager.Instance.TrashBinZone))
        {
            OnIngredientDestroy();
            return;
        }

        if (OverlapsWithZone(GameManager.Instance.AssemblyZone))
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

        Debug.Log("Images do not overlap.");
        return false;
    }
    

}
