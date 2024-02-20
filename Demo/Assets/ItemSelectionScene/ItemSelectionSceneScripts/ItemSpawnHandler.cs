using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemUtil;
using UnityEngine.UI;

public class ItemSpawnHandler : MonoBehaviour
{
    
    private GameObject[] ShopInventory;
    
    
    public GameObject EmptyItemPrefab;
    
    int maxItems = 8;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        //Generate a new item pool
        ItemPool.GenerateNewitemPool();
        
        //Get shop inventory slots
        GameObject ButtonCanvas = GameObject.Find("ButtonCanvas");
        ShopInventory = GameObject.Find("ButtonCanvas/Shop").GetComponent<CanvasInventoryScript>().InventorySlots;
        

        for (int j = 0; j < maxItems; j++)
        {
            //Create draggable item gameobject
            GameObject newItem = Instantiate(EmptyItemPrefab, ButtonCanvas.transform) as GameObject;

            //Pull random gameitem from the ItemPool
            GameItem P = ItemPool.PullRandomItem();

            //Assign gameitem to gameobject. Adds UI elements
            newItem.GetComponent<Image>().sprite = P.getItemImage();
            newItem.GetComponent<ItemDragDropScript>().CurrentItem = P;

            //Assign draggable gameobject to the shop inventory
            ShopInventory[j].GetComponent<InventorySingleboxScript>().AssignItemToSlot(newItem);
        }
        
    }

}
