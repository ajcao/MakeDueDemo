using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ItemUtil;

public class InventorySingleboxScript : MonoBehaviour, IDropHandler
{
    public GameObject ItemInSlot;
    
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
            
            //Global item in global item slot
            if (this.gameObject.GetComponent<Transform>().parent.gameObject.name == "GlobalItems" && eventData.pointerDrag.GetComponent<ItemDragDropScript>().GetAssignedItem().isGlobal)
            {
                eventData.pointerDrag.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
                eventData.pointerDrag.GetComponent<ItemDragDropScript>().SetDefault();
                eventData.pointerDrag.GetComponent<ItemDragDropScript>().ItemSlot = this.gameObject;
                ItemInSlot = eventData.pointerDrag.gameObject;
                
            }
            
            //Normal intem in character slot
            else if (this.gameObject.GetComponent<Transform>().parent.gameObject.name != "GlobalItems" && !eventData.pointerDrag.GetComponent<ItemDragDropScript>().GetAssignedItem().isGlobal)
            {
                eventData.pointerDrag.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
                eventData.pointerDrag.GetComponent<ItemDragDropScript>().SetDefault();
                eventData.pointerDrag.GetComponent<ItemDragDropScript>().ItemSlot = this.gameObject;
                ItemInSlot = eventData.pointerDrag.gameObject;
            }
            
            //Return to default position
            else
            {
                eventData.pointerDrag.GetComponent<ItemDragDropScript>().ReturnToDefaultPosition();
            }
        }
    }
}
