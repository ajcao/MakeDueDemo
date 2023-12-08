using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemUtil;
using UnityEngine.UI;

public class ItemSpawnHandler : MonoBehaviour
{
    public GameObject[] AllItems;
    
    private GameObject[] ShopInventory;
    
    
    public GameObject EmptyItemPrefab;
    
    int maxItems = 8;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        //Get Item pool
        List<GameItem> AllPotentialItems = EntireLibraryItem.GetAllItems();
        
        //Get shop inventory slots
        GameObject ButtonCanvas = GameObject.Find("ButtonCanvas");
        ShopInventory = GameObject.Find("ButtonCanvas/Shop").GetComponent<CanvasInventoryScript>().InventorySlots;
        
        //List for current item stocks in the shop
        GameItem[] CurrentItems = new GameItem[maxItems];
        
        //Create the list of current items by randomly drawing from Item Pool
        int i = 0;
        while (AllPotentialItems.Count > 0 && i < maxItems)
        {
            int r = Random.Range(0, AllPotentialItems.Count);
            GameItem P = AllPotentialItems[r];
            CurrentItems[i] = P;
            AllPotentialItems.Remove(P);
            i++;
        }
        
        //Using create list of created items
        //Create a draggable GameObject that represents the current item
        //Place the draggable GameObject in the shop
        for (int j = 0; j < maxItems; j++)
        {
            GameObject newItem = Instantiate(EmptyItemPrefab, ButtonCanvas.transform) as GameObject;
            newItem.GetComponent<Image>().sprite = CurrentItems[j].getItemImage();
            newItem.GetComponent<ItemDragDropScript>().CurrentItem = CurrentItems[j];
            ShopInventory[j].GetComponent<InventorySingleboxScript>().AssignItemToSlot(newItem);
        }
        
    }

}
