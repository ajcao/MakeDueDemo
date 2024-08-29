using CharacterUtil;
using ItemUtil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondItemSelectionSceneHandler : MonoBehaviour
{
    //Second Item Selection Screen is highly simplified
    //due to the fact that everything has been initalized
    
    public CanvasInventoryScript[] AllCharacterInventories;
    private GameObject[] ShopInventory;
    int maxItems = 8;

    public GameObject EmptyItemPrefab;


    void Start()
    {

        //Generate second set of items
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
        this.SetPlayerGameObjectToCorrectLocation();
        this.SetPlayerGameObjectToCanvasInventory();
        this.SetCurrentInventoryNull();

        //Allow the plalyer to access character information
        for (int i = 0; i < 4; i++)
        {
            //Click on character no longer opens up Character Data scene, avoids potential crashes
            PlayerParty.getPartyMember(i).GetComponent<PlayableCharacter>().ToggleCharacterData(true);
        }

    }

    //Draw the correct player gameobject to the correct location
    //ASSUMES PLAYER PARTY HAS BEEN INITALIZE
    public void SetPlayerGameObjectToCorrectLocation()
    {

        for (int i = 0; i < 4; i++)
        {
            GameObject Player = PlayerParty.getPartyMember(i);
            
            Player.transform.position = new Vector3(-6f + (4.0f * i),3.0f, 0.0f);
        }
    }

    //Sync up the correct player gameobject to the CanvasInventory script
    //ASSUMES PLAYER PARTY HAS BEEN INITALIZE
    public void SetPlayerGameObjectToCanvasInventory()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject ItemBox;
            GameObject Player = PlayerParty.getPartyMember(i);
            
            AllCharacterInventories[i].Player = Player;

            ItemBox = AllCharacterInventories[i].InventorySlots[0];
            ItemBox.transform.position = Camera.main.WorldToScreenPoint(Player.transform.position + new Vector3(-1.0f,-1.5f,0.0f));

            ItemBox = AllCharacterInventories[i].InventorySlots[1];
            ItemBox.transform.position = Camera.main.WorldToScreenPoint(Player.transform.position + new Vector3(0.0f,-1.5f,0.0f));
            
            ItemBox = AllCharacterInventories[i].InventorySlots[2];
            ItemBox.transform.position = Camera.main.WorldToScreenPoint(Player.transform.position + new Vector3(1.0f,-1.5f,0.0f));
        }
        
    }

    public void SetCurrentInventoryNull()
    {
        //Generate second set of items
        //Get shop inventory slots
        GameObject ButtonCanvas = GameObject.Find("ButtonCanvas");
        ShopInventory = GameObject.Find("ButtonCanvas/Shop").GetComponent<CanvasInventoryScript>().InventorySlots;

        for (int i = 0; i < 4; i++)
        {
            GameObject ItemBox;
            GameObject Player = PlayerParty.getPartyMember(i);

            AllCharacterInventories[i].Player = Player;
            int j = 0;
            foreach (GameItem GI in Player.GetComponent<PlayableCharacter>().GetIntentory())
            {
                GameObject newItem = Instantiate(EmptyItemPrefab, ButtonCanvas.transform) as GameObject;
                newItem.GetComponent<Image>().sprite = GI.getItemImage();
                newItem.GetComponent<ItemDragDropScript>().CurrentItem = GI;
                newItem.GetComponent<ItemDragDropScript>().disableItem();
                AllCharacterInventories[i].InventorySlots[j].GetComponent<InventorySingleboxScript>().AssignItemToSlot(newItem);
                j++;
            }

        }
    }



}
