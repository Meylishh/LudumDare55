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

    public void OnEndDrag(PointerEventData eventData)
    {
        CheckOverlapWithZone(IngredientBank.Instance.WorkspaceZone);
    }

    private void CheckOverlapWithZone(RectTransform zoneRect)
    {
        var childPosition = draggableObjectRectTransform.localPosition;
        var parentMin = zoneRect.rect.min;
        var parentMax = zoneRect.rect.max;

        if (childPosition.x < parentMin.x || childPosition.x > parentMax.x ||
            childPosition.y < parentMin.y || childPosition.y > parentMax.y)
        {
            Debug.Log("Child image has left the bounds of the parent image.");

            draggableObjectRectTransform.anchoredPosition = lastFixedPosition;
        }
        else
        {
            lastFixedPosition = draggableObjectRectTransform.anchoredPosition;
            draggableObjectRectTransform.anchoredPosition = lastFixedPosition;
        }
    }
    
}
