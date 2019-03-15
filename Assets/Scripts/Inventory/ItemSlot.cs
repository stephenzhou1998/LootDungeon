using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public string type;
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop (PointerEventData eventData)
    {
        // Only put item in slot if it's empty and it's an inventory slot or its equipment slot type matches the item's type.
        if (!item && (type.Equals("Inventory") || type.Equals(DragHandler.itemBeingDragged.GetComponent<Item>().type)))
        {
            Transform t = DragHandler.itemBeingDragged.transform;
            t.SetParent(transform);
            RectTransform rt = t.GetComponent<RectTransform>();
            RectTransform prt = transform.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(prt.sizeDelta.x * 0.9f, prt.sizeDelta.y * 0.9f);

            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x,y) => x.HasChanged());
        }
    }

    public void OnMouseEnter ()
    {
        Debug.Log("mouse enter");
        if (!item)
        {
            transform.GetComponentInParent<Inventory>().descriptionText.text = "Empty";
        } else
        {
            transform.GetComponentInParent<Inventory>().descriptionText.text = item.GetComponent<Item>().description;
        }
    }

    public void OnMouseExit ()
    {
        Debug.Log("mouse exit");
        transform.GetComponentInParent<Inventory>().descriptionText.text = "";
    }
}
