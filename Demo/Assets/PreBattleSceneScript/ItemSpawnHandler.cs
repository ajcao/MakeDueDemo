using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemUtil;
using UnityEngine.UI;

public class ItemSpawnHandler : MonoBehaviour
{
    public GameObject[] AllItems;
    
    private GameObject[] ShopInventory;
    
    public EntireLibraryItem ItemLibrary;
    
    public GameObject EmptyItemPrefab;
    
    int maxItems = 5;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        GameObject ButtonCanvas = GameObject.Find("ButtonCanvas");
        ShopInventory = GameObject.Find("ButtonCanvas/Shop").GetComponent<CanvasInventoryScript>().InventorySlots;
        List<GameItem> AllPotentialItems = ItemLibrary.GetAllItems();
        
        GameItem[] CurrentItems = new GameItem[maxItems];
        
        int i = 0;
        while (AllPotentialItems.Count > 0 && i < maxItems)
        {
            int r = Random.Range(0, AllPotentialItems.Count);
            GameItem P = AllPotentialItems[r];
            CurrentItems[i] = P;
            AllPotentialItems.Remove(P);
            i++;
        }
        
        for (int j = 0; j < maxItems; j++)
        {
            GameObject newItem = Instantiate(EmptyItemPrefab, ButtonCanvas.transform) as GameObject;
            newItem.GetComponent<Image>().sprite = CurrentItems[j].getItemImage();
            newItem.GetComponent<ItemDragDropScript>().CurrentItem = CurrentItems[j];
            ShopInventory[j].GetComponent<InventorySingleboxScript>().AssignItemToSlot(newItem);
        }
        
    }

}
