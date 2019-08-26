﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;
    Transform last;

    void Start()
    {
        last = GameObject.Find("InventoryCanvas").transform.Find("Inventory").Find("Last");
    }

    public Transform getStartParent()
    {
        return startParent;
    }

    public void OnBeginDrag (PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(last);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag (PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == last)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
    }
}
