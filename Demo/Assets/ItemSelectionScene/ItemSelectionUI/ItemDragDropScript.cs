using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ItemUtil;
using UnityEngine.UI;
using TooltipUtil;

public class ItemDragDropScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, TooltipStringInterface
{
    public Vector3 DefaultPosition;
    
    public GameItem CurrentItem;
    
    bool isDragged = false;
    
    public GameObject ItemSlot;
    
    public void AssignItem(GameItem I)
    {
        CurrentItem = I;
    }
    
    public GameItem GetAssignedItem()
    {
        return CurrentItem;
    }
    
    public void SetDefault()
    {
        DefaultPosition = this.gameObject.transform.position;
    }
    
    public void ReturnToDefaultPosition()
    {
        this.gameObject.transform.position = DefaultPosition;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        this.isDragged = true;
        this.GetComponent<Image>().raycastTarget = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        this.gameObject.transform.position = Input.mousePosition;
        
        //Disable tooltips if one pops up
        if (eventData.pointerDrag.TryGetComponent(out TooltipScript textScript))
        {
            textScript.ResetToolTip();
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragged = false;
        this.GetComponent<Image>().raycastTarget = true;
    }
    
    //If the item was not dropped in a box as a last resort reset position if needed
    public void LateUpdate()
    {
        if ((this.transform.position != ItemSlot.transform.position) && (isDragged == false))
        {
            this.transform.position = ItemSlot.transform.position;
        }
    }
    
    public string GetTooltipString()
    {
        return CurrentItem.GetTooltipString();
    }
}
