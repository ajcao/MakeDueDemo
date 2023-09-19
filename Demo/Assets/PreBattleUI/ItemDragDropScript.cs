using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ItemUtil;
using UnityEngine.UI;

public class ItemDragDropScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
        Debug.Log("Begin Drag");
        Debug.Log(eventData.pointerDrag);
        this.isDragged = true;
        this.GetComponent<Image>().raycastTarget = false;
        this.GetComponent<CanvasGroup>().alpha = 0.5f;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        Debug.Log(eventData.pointerDrag);
        this.gameObject.transform.position = Input.mousePosition;
        
        //Disable tooltips if one popes up
        if (eventData.pointerDrag.TryGetComponent(out TooltipScript textScript))
        {
            textScript.ResetToolTip();
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        Debug.Log(eventData.pointerDrag);
        isDragged = false;
        this.GetComponent<Image>().raycastTarget = true;
        this.GetComponent<CanvasGroup>().alpha = 1f;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    
    //If the item was not dropped in a box as a last resort reset position if needed
    public void LateUpdate()
    {
        if ((this.transform.position != DefaultPosition) && (isDragged == false))
        {
            this.ReturnToDefaultPosition();
        }
    }
}
