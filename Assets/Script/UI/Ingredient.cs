using System;
using System.Collections;
using System.Collections.Generic;
using Script.Scroll;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ingredient : MonoBehaviour, IDragHandler, IEndDragHandler
{
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
        //todo: check overlap with trash bin
        /*if (OverlapsWithAssembly(IngredientBank.Instance.AssemblyZone))
        {
            Debug.Log("Assembly overlap");
            return;
        }
        
        if (!OverlapsWithWorkspace(IngredientBank.Instance.WorkspaceZone))
        {
            draggableObjectRectTransform.anchoredPosition = lastFixedPosition;
        }
        else
        {
            lastFixedPosition = draggableObjectRectTransform.anchoredPosition;
            draggableObjectRectTransform.anchoredPosition = lastFixedPosition;
        }*/

        if (OverlapsWithZone(IngredientBank.Instance.AssemblyZone))
        {
            IngredientBank.Instance.Burger.AddIngredient(gameObject);
            return;
        }
        
        if (OverlapsWithZone(IngredientBank.Instance.WorkspaceZone))
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
            Debug.Log("Images overlap!");
            return true;
        }
        else
        {
            Debug.Log("Images do not overlap.");
            return false;
        }
    }
    

}
