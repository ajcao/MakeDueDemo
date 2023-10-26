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
    
    public NextTurnButtonScript NextTurnButton;
    
    public Texture2D TargetCrosshairEnemy;
    public Texture2D TargetCrosshairPlayer;
    
    protected bool IsCastingMode = false;
    
    
    
    public void Start()
    {
        //Assigns giving the character gameobject to the buttons
        for (int i = 0; i < PlayerParty.getPartySize(); i++)
        {
            GameObject C = PlayerParty.getPartyMember(i);
            PlayableCharacter CBehavior = C.GetComponent<PlayableCharacter>();
            
            SelectCharacterButtonList[i].GetComponent<SelectCharacterButtonScript>().Init(CBehavior, this);
        }
        
        foreach (AbilityButtonScript A in AbilityButtonList)
        {
            A.Init(this);
        }
        
        NextTurnButton.Init(this);
            
    }
    
    public void SetCastingMode(bool v)
    {
        IsCastingMode = v;
    }
    
    public bool IsInCastingMode()
    {
        return IsCastingMode;
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
    
    public void ResetCasting()
    {
        currentCharacter = null;
        currentTarget = null;
        currentAbility = null;
        
        StopAllCoroutines();
        this.SetCastingMode(false);
    }
    
    //Starts the gamestate where the game is waiting
    //for all characters to casts.
    //Enables the player to interact with the buttons
    public void StartCastingMode()
    {
        IsCastingMode = true;
        Sprite ButtonImage = Resources.Load<Sprite>("NextTurnButtonImage") as Sprite;
        NextTurnButton.gameObject.GetComponent<Image>().sprite = ButtonImage;
        NextTurnButton.gameObject.gameObject.GetComponent<Button>().interactable = true;
        StartCoroutine(CharacterCasting());
    }

    IEnumerator CharacterCasting()
    {
        while (currentTarget == null)
        {
            //User must have ability selected (via ability button handler) when they click a target
            //Alternatively the players may have used an abillity that requires no target
            if ( (currentAbility != null) && (currentAbility.getTargetingType() == TargetingTypeEnum.NoTarget || Input.GetMouseButtonDown(0)) )
            {
                if (currentAbility.getTargetingType() == TargetingTypeEnum.NoTarget)
                {
                    currentTarget = (Character) currentCharacter;
                    currentAbility.onCast(currentTarget);
                    currentAbility.postCastWrapper(currentTarget);
                }
                
                
                else //Case where user clicked on something
                {
                    //Define target to be hit by ability by creating raycast line aimed at the mousecursor
                    //Using this method, no character needs a button component
                    RaycastHit2D click = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity);
                    
                    //Cond 1: User clicked on something with a collider 
                    //Cond 2: Abilities has this specific targeting type
                    //Cond 3: Collider has the correct tag (either EnemyCharacter, PlayableCharacter or both?)
                    if (click.collider != null && currentAbility.getTargetingType() == TargetingTypeEnum.EnemyTarget && click.collider.gameObject.tag == "EnemyCharacter")
                    {
                        currentTarget = click.collider.gameObject.GetComponent<EnemyCharacter>();
                        currentAbility.onCast((Character) currentTarget);
                        currentAbility.postCastWrapper((Character) currentTarget);
                    }
                    else if (click.collider != null && currentAbility.getTargetingType() == TargetingTypeEnum.PlayerTarget && click.collider.gameObject.tag == "PlayableCharacter")
                    {
                        currentTarget = click.collider.gameObject.GetComponent<PlayableCharacter>();
                        currentAbility.onCast((Character) currentTarget);
                        currentAbility.postCastWrapper((Character) currentTarget);
                    }
                }
            }
            yield return null;
        }
        
        currentCharacter = null;
        currentTarget = null;
        currentAbility = null;
        
        if (CanSomeoneCast() == true && !EnemyEncounter.IsEncounterDead())
        {
            StartCoroutine(CharacterCasting());
        }
    }
    
    public bool CanSomeoneCast()
    {
        //If all enemies are dead, there is no more need to cast
        if (EnemyEncounter.IsEncounterDead())
        {
            return false;
        }
        
        foreach (GameObject G in PlayerParty.getParty())
        {
            if (G.GetComponent<PlayableCharacter>().IsAbletoCast())
                return true;
        }
        return false;
    }
    
    //Update ability buttons to always display current Characters Abilities
    //Also handles colors of Abillity buttons
    //Does not handle color of SelectCharacter button
    //Should likely move this to each respective button script
    public void Update()
    {
        //Change cursor
        if (currentAbility == null)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  
        }
        else if (currentAbility.getTargetingType() == TargetingTypeEnum.EnemyTarget)
        {
            Cursor.SetCursor(TargetCrosshairEnemy, new Vector2(TargetCrosshairEnemy.width / 2, TargetCrosshairEnemy.height / 2), CursorMode.Auto);
        }
        else if (currentAbility.getTargetingType() == TargetingTypeEnum.PlayerTarget)
        {
            Cursor.SetCursor(TargetCrosshairPlayer, new Vector2(TargetCrosshairPlayer.width / 2, TargetCrosshairPlayer.height / 2), CursorMode.Auto);
        }
        
        //Makes character selector button grey or not
        foreach (SelectCharacterButtonScript SC in SelectCharacterButtonList)
        {
            
            if (!SC.GetAssignedCharacter().IsAbletoCast())
            {
                  SC.gameObject.GetComponent<Image>().color = Color.gray;
            }
            else
            {
                SC.gameObject.GetComponent<Image>().color = Color.white;
            }
            
            if (SC.GetAssignedCharacter() == currentCharacter)
            {
                SC.gameObject.GetComponent<Image>().color = Color.yellow;
            }
            
        }
        
        //Highlighes current abilities
        if (currentCharacter != null)
        {
            for (int i = 0; i < AbilityButtonList.Length; i++)
            {
                GameObject AbilityButton = AbilityButtonList[i].gameObject;
                AbilityButton.SetActive(true);
                Ability A = currentCharacter.getAbilityPool()[i];
                AbilityButton.GetComponent<AbilityButtonScript>().DefineAbility(A);
                AbilityButton.GetComponent<Image>().sprite = A.getIcon();
                
                //Determines whether buttons castable and thus intertable
                //Checks if owning character is able to cast
                if (!currentCharacter.IsAbletoCast())
                {
                    AbilityButton.GetComponent<Button>().interactable = false;
                }
                //Abilities that cannot be cast for internal reasons (cooldown, etc)
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
