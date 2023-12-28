using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBattleSceneHandler : MonoBehaviour
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
    void Start()
    {        
        //Reset BattleScene
        SceneCoordinator.ResetBattleStatus();
        
        
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
        
        //Draw party on screen
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[0], new Vector2(-6f,3.0f), Quaternion.identity) as GameObject));
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[1], new Vector2(-2f,3.0f), Quaternion.identity) as GameObject));
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[2], new Vector2(2f,3.0f), Quaternion.identity) as GameObject));
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[3], new Vector2(6f,3.0f), Quaternion.identity) as GameObject));
        
        //Set Inventory to Proper Locations
        for (i = 0; i < 4; i++)
        {
            GameObject ItemBox;
            GameObject Player = PlayerParty.getPartyMember(i);
            
            ItemBox = AllCharacterInventories[i].InventorySlots[0];
            ItemBox.transform.position = Camera.main.WorldToScreenPoint(Player.transform.position + new Vector3(-1.0f,-1.5f,0.0f));

            ItemBox = AllCharacterInventories[i].InventorySlots[1];
            ItemBox.transform.position = Camera.main.WorldToScreenPoint(Player.transform.position + new Vector3(0.0f,-1.5f,0.0f));
            
            ItemBox = AllCharacterInventories[i].InventorySlots[2];
            ItemBox.transform.position = Camera.main.WorldToScreenPoint(Player.transform.position + new Vector3(1.0f,-1.5f,0.0f));
        }
    }


}
