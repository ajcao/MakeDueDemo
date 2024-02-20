using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterUtil;
using AbilityUtil;
using TMPro;

//Current this scene is enter by the OnMouseDown function in PlayableCharacter
//Should find a way to move character detail button to here. 

public class CharacterDataSceneHandler : MonoBehaviour
{    
    private PlayableCharacter currentCharacter;
    
    public AbilityButtonScript[] AbilityButtonList;
    
    
    
    public void Start()
    {
        currentCharacter = GameObject.Find("DummyCharacterGameObject").GetComponent<PlayableCharacter>();
        currentCharacter.transform.position = new Vector3(-8.0f, 1.0f, 0.0f);
        
        //Set Dummy character to have no ToggleCharacterData
        currentCharacter.ToggleCharacterData(false);
        
        //Set Item Selection Screen UI to be invisible
        GameObject.Find("ButtonCanvas").GetComponent<Canvas>().enabled = false;
        
        //Move player characters do different location
        for (int i = 0; i < 4; i++)
        {
            PlayerParty.getPartyMember(i).transform.position = new Vector3(0, 200, 0);
        }
        
        //Set TextBox
        GameObject.Find("CharacterDataCanvas").GetComponent<TextMeshProUGUI>().text = currentCharacter.GetLoreData();
        
        for (int i = 0; i < AbilityButtonList.Length; i++)
        {
            GameObject AbilityButton = AbilityButtonList[i].gameObject;
            AbilityButton.SetActive(true);
            //Offset since the first 3 abilites are resolve, basic attack, and basic defend
            Ability A = currentCharacter.getAbilityPool()[i+3];
            AbilityButton.GetComponent<AbilityButtonScript>().DefineAbility(A);
            AbilityButton.GetComponent<Image>().sprite = A.getIcon();
            
        }
    }
    
}
