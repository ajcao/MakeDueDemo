using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterUtil;
using AbilityUtil;

public class AbilityButtonHandler : MonoBehaviour
{
    public GameObject SelectCharacterButtonPrefab;
    
    public GameObject AbilityButtonPrefab;
    //Variables that handle Abilities being casted and on who
    
    private PlayableCharacter currentCharacter;
    
    private Ability currentAbility;
    
    private Character currentTarget;
    
    public SelectCharacterButtonScript[] SelectCharacterButtonList;
    
    public AbilityButtonScript[] AbilityButtonList;
    
    
    public void Start()
    {
        //Init Character Selection Buttons
        for (int i = 0; i < PlayerParty.getPartySize(); i++)
        {
            GameObject C = PlayerParty.getPartyMember(i);
            PlayableCharacter CBehavior = C.GetComponent<PlayableCharacter>();
            
            SelectCharacterButtonList[i].GetComponent<SelectCharacterButtonScript>().Init(CBehavior, this);
            
            SelectCharacterButtonList[i].GetComponent<Image>().sprite = CBehavior.getCharacterIcon();
        }
        
        //Temporarily Setting Max Spell Slots to 4
        //May need to handle cases for Characters with Variable Num of Spelll Slots
        for (int i = 0; i < 4; i++)
        {
            //GameObject AbilityButton = Instantiate(AbilityButtonPrefab, this.gameObject.transform, false) as GameObject;
            //AbilityButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300+(300*i), -150);
            AbilityButtonList[i].GetComponent<AbilityButtonScript>().Init(this);
        }
            
            
    }
    
    public void SetCurrentCharacter(PlayableCharacter C)
    {
        currentCharacter = C;
    }
    
    public PlayableCharacter GetCurrentCharacter()
    {
        return currentCharacter;
    }
    
    public void SetCurrentAbility(Ability A)
    {
        currentAbility = A;
    }
    
    public Ability GetCurrentAbility()
    {
        return currentAbility;
    }
    
    public void StartCastingMode()
    {
        StartCoroutine(CharacterCasting());
    }

    //Temporariy function to test that casting works as expected
    IEnumerator CharacterCasting()
    {
        while (currentTarget == null)
        {
            //User must have ability selected when they click a target
            if ((currentAbility != null) & Input.GetMouseButtonDown(0))
            {
                
                //Define target to be hit by ability
                RaycastHit2D click = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity);
                if (click.collider != null && currentAbility.getTargetingType() == TargetingTypeEnum.EnemyTarget && click.collider.gameObject.tag == "EnemyCharacter")
                {
                    currentTarget = click.collider.gameObject.GetComponent<EnemyCharacter>();;
                    currentAbility.getPlayableCharacter().setHasCasted(true);
                    currentAbility.onCast((Character) currentTarget);
                }
                else if (click.collider != null && currentAbility.getTargetingType() == TargetingTypeEnum.PlayerTarget && click.collider.gameObject.tag == "PlayableCharacter")
                {
                    currentTarget = click.collider.gameObject.GetComponent<PlayableCharacter>();
                    currentAbility.getPlayableCharacter().setHasCasted(true);
                    currentAbility.onCast((Character) currentTarget);
                }
            }
            yield return null;
        }
        currentAbility.getPlayableCharacter().RefreshCast();
        
        currentCharacter = null;
        currentTarget = null;
        currentAbility = null;
        
        if (EveryoneHasCasted() == false)
        {
            StartCoroutine(CharacterCasting());
        }
    }
    
    public bool EveryoneHasCasted()
    {
        bool P1Cast = PlayerParty.getPartyMember(0).GetComponent<PlayableCharacter>().getHasCasted();
        bool P2Cast = PlayerParty.getPartyMember(1).GetComponent<PlayableCharacter>().getHasCasted();
        bool P3Cast = PlayerParty.getPartyMember(2).GetComponent<PlayableCharacter>().getHasCasted();
        bool P4Cast = PlayerParty.getPartyMember(3).GetComponent<PlayableCharacter>().getHasCasted();
        return (P1Cast && P2Cast && P3Cast && P4Cast);
    }
    
    public void ResetEveryoneCast()
    {
        PlayerParty.getPartyMember(0).GetComponent<PlayableCharacter>().setHasCasted(false);
        PlayerParty.getPartyMember(0).GetComponent<PlayableCharacter>().resetProtectionList();
        
        PlayerParty.getPartyMember(1).GetComponent<PlayableCharacter>().setHasCasted(false);
        PlayerParty.getPartyMember(1).GetComponent<PlayableCharacter>().resetProtectionList();
        
        PlayerParty.getPartyMember(2).GetComponent<PlayableCharacter>().setHasCasted(false);
        PlayerParty.getPartyMember(2).GetComponent<PlayableCharacter>().resetProtectionList();
        
        PlayerParty.getPartyMember(3).GetComponent<PlayableCharacter>().setHasCasted(false);
        PlayerParty.getPartyMember(3).GetComponent<PlayableCharacter>().resetProtectionList();
    }
    
    //Update ability buttons to always display current Characters Abilities
    //Also handles colors of Abillity buttons
    //Does not handle color of SelectCharacter button
    public void Update()
    {                
        if (currentCharacter != null)
        {
            for (int i = 0; i < AbilityButtonList.Length; i++)
            {
                GameObject AbilityButton = AbilityButtonList[i].gameObject;
                AbilityButton.SetActive(true);
                Ability A = currentCharacter.getAbilityPool()[i];
                AbilityButton.GetComponent<AbilityButtonScript>().DefineAbility(A);
                AbilityButton.GetComponent<Image>().sprite = A.getIcon();
                
                //Determines whether an ability is castable and thus intertable
                //Checks if owning character is able to cast
                if (currentCharacter.getHasCasted() == true)
                {
                    AbilityButton.GetComponent<Button>().interactable = false;
                }
                //Abilities that cannot be cast for internal reasons (mana cost, cooldown, etc)
                //are displayed but cannot be cast
                else if (A.canCast() == false)
                {
                    AbilityButton.GetComponent<Button>().interactable = false;
                }
                else
                {
                    AbilityButton.GetComponent<Button>().interactable = true;
                }
                
                //Highllight current Ability
                if (currentAbility == A)
                {
                    AbilityButton.GetComponent<Image>().color = Color.yellow;
                }
                else
                {
                    AbilityButton.GetComponent<Image>().color = Color.white;
                }
                
            }
        }
        else //If no character is selected hide buttons
        {
            for (int i = 0; i < AbilityButtonList.Length; i++)
            {
                GameObject AbilityButton = AbilityButtonList[i].gameObject;
                AbilityButton.SetActive(false);
            }
            
        }
    }
}
