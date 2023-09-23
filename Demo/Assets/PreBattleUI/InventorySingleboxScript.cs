using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ItemUtil;

public class InventorySingleboxScript : MonoBehaviour, IDropHandler
{
    public GameObject ItemInSlot = null;
    
    public void AssignItemToSlot(GameObject I)
    {
        ItemInSlot = I;
        I.GetComponent<ItemDragDropScript>().ItemSlot = this.gameObject;
        I.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
        I.GetComponent<ItemDragDropScript>().SetDefault();
    }
    
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        Debug.Log(eventData.pointerDrag);
        //There is an item
        if (eventData.pointerDrag != null)
        {
            //Determine itemslot is empty;
            Debug.Log(ItemInSlot);
            if (ItemInSlot != null)
            {
               eventData.pointerDrag.GetComponent<ItemDragDropScript>().ReturnToDefaultPosition(); 
            }
            else
            {
                //Reset previous inventory box
                eventData.pointerDrag.GetComponent<ItemDragDropScript>().ItemSlot.GetComponent<InventorySingleboxScript>().ItemInSlot = null;
                
                
                eventData.pointerDrag.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
                eventData.pointerDrag.GetComponent<ItemDragDropScript>().SetDefault();
                eventData.pointerDrag.GetComponent<ItemDragDropScript>().ItemSlot = this.gameObject;
                ItemInSlot = eventData.pointerDrag.gameObject;
                
            }
        }
    }
}
