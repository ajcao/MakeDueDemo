using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectionSceneHandler : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;
    public GameObject P5;
    public GameObject P6;
    public GameObject P7;
    public GameObject P8;
    
    public CanvasInventoryScript[] AllCharacterInventories;
    
    
    // Start is called before the first frame update
    //This scene is only entered when a new round has started
    void Start()
    {        
        //Reset BattleScene
        SceneCoordinator.ResetBattleStatus();

        //Generate new party if this is the player's first game
        //Otherwise keep the previous party but generate new gameobjects
        if (PlayerParty.CheckPartyEmpty())
            this.GenerateRandomParty();
        else
        {
            GameObject Player0 = (Instantiate(PlayerParty.getPartyMember(0)) as GameObject);
            GameObject Player1 = (Instantiate(PlayerParty.getPartyMember(1)) as GameObject); 
            GameObject Player2 = (Instantiate(PlayerParty.getPartyMember(2)) as GameObject); 
            GameObject Player3 = (Instantiate(PlayerParty.getPartyMember(3)) as GameObject); 

            //Deletes the current party
            PlayerParty.DeleteParty();

            //Adds new party
            PlayerParty.AddPartyMember(Player0);
            PlayerParty.AddPartyMember(Player1);
            PlayerParty.AddPartyMember(Player2);
            PlayerParty.AddPartyMember(Player3);
            
            this.SetPlayerGameObjectToCorrectLocation();
            this.SetPlayerGameObjectToCanvasInventory();
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


    //Overwrites the PlayerParty static class data
    //This should be moved to a new location
    public void GenerateRandomParty()
    {
        //Deletes the current party
        PlayerParty.DeleteParty();

        List<GameObject> TotalCharacterArray = new List<GameObject>();
        TotalCharacterArray.Add(P1);
        TotalCharacterArray.Add(P2);
        TotalCharacterArray.Add(P3);
        TotalCharacterArray.Add(P4);
        TotalCharacterArray.Add(P5);
        TotalCharacterArray.Add(P6);
        TotalCharacterArray.Add(P7);
        TotalCharacterArray.Add(P8);
        
        GameObject[] CurrentCharacterArray = new GameObject[4];
        
        //Draft random party of 4 characters from poll of 8
        int i = 0;
        while (TotalCharacterArray.Count > 0 && i < 4)
        {
            int r = Random.Range(0, TotalCharacterArray.Count);
            GameObject P = TotalCharacterArray[r];
            CurrentCharacterArray[i] = P;
            TotalCharacterArray.Remove(P);
            i++;
        }
        
        //Instantiate the player characters as new game object prefab
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[0]) as GameObject));
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[1]) as GameObject));
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[2]) as GameObject));
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[3]) as GameObject));

        this.SetPlayerGameObjectToCorrectLocation();
        this.SetPlayerGameObjectToCanvasInventory();
    }


}
