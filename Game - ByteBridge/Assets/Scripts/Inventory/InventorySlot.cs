using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot: MonoBehaviour, IPointerDownHandler
{
    public int slotId;
    public bool initialised;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Got mousedown");
        if (initialised)
        {
            LoadoutRNGManager.Instance.RemoveGunFromInventory(slotId);    
        }
    }

}    