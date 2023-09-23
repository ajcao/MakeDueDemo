using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using UnityEngine.SceneManagement;
using ItemUtil;

public class StartBattleButtonScript : MonoBehaviour
{
    public CanvasInventoryScript[] AllCharacterInventories;
    
    public void OnButtonClick()
    {
        Debug.Log("Button Click");
        foreach (GameObject PC in PlayerParty.getParty())
        {
            DontDestroyOnLoad(PC);
        }
        
        //Assign Items to characters
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject DragItem = AllCharacterInventories[i].InventorySlots[j].GetComponent<InventorySingleboxScript>().ItemInSlot;
                if (DragItem != null)
                {
                    GameItem I = DragItem.GetComponent<ItemDragDropScript>().CurrentItem;
                    PlayerParty.getPartyMember(i).GetComponent<PlayableCharacter>().AddToInventory(I);
                }
            }
        }
        
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
    }
}
