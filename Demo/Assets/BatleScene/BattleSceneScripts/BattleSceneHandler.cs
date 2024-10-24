using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using UnityEngine.UI;
using ItemUtil;
using Unity.VisualScripting.FullSerializer;

public class BattleSceneHandler : MonoBehaviour
{
    
    public GameObject E1;
    public GameObject E2;
    
    public static int Round;
    private bool isBattling;
    public bool isTutorial;
    
    public AbilityButtonHandler AB_Handler;
    
    public EnemyMoveHandler EM_Handler;
    
    public HealthArmorHandler HA_Handler;

    public BattleTurnIndicatorScript BT_Indicator;
    
    public delegate void EndGameDelegate();
    public static EndGameDelegate EndGame;
    
    public static int GetRound()
    {
        return Round;
    }
    
    public void EndGameMethod()
    {
        StopAllCoroutines();
        isBattling = false;

        foreach (GameObject G in PlayerParty.GetLivingPartyMembers())
        {
            Character P = G.GetComponent<PlayableCharacter>();
            BuffHandler.RemoveAllBuffsFromCharacter(P);
            
            Object.Destroy(G.GetComponentInChildren<HealthArmorScript>().gameObject);
        }

        if (PlayerParty.IsPartyDead() || isTutorial || SceneCoordinator.FinalBossFightAttempted)
        {
            //Changes the End Turn button to move to return to the title scene
            AB_Handler.NextTurnButton.gameObject.GetComponent<NextTurnButtonScript>().EditButtonFunction("ReturnToTitleScene");
        }
        else
        {
            //Changes the End Turn button to move to next battle scene
            AB_Handler.NextTurnButton.gameObject.GetComponent<NextTurnButtonScript>().EditButtonFunction("GoToNextScene");
        }
        Round = -1;
    }
    
    public void Awake()
    {
        //Add method to delegate for ending the game
        EndGame = EndGameMethod;

        BattleLogicHandler.Init();

        //In Tutorial
        if (isTutorial)
        {
            //Deletes the current party
            PlayerParty.DeleteParty();
            
            //Creates a party for the tutorial
            PlayerLibraryScript PLS = GameObject.Find("PlayerLibrary").GetComponent<PlayerLibraryScript>();

            GameObject[] CurrentCharacterArray = PlayerParty.GenerateParty(PLS);

            //Instantiate the player characters as new game object prefab
            PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[0]) as GameObject));
            PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[1]) as GameObject));
            PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[2]) as GameObject));
            PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[3]) as GameObject));

            //Kill the 2 characters so player party resembles 2
            GameObject[] Party = PlayerParty.getParty();
            Party[0].GetComponent<Character>().onDeath();
            Party[0].GetComponent<Character>().transform.position = new Vector3(0, -500, 0);
            AB_Handler.HideCharacterSelectionDuringTutorial(0);

            Party[1].GetComponent<Character>().onDeath();
            Party[1].GetComponent<Character>().transform.position = new Vector3(0, -500, 0);
            AB_Handler.HideCharacterSelectionDuringTutorial(1);

        }

        //Place Player in correct location
        PlayerParty.getPartyMember(0).transform.position = new Vector3(-8.0f, 0.0f, 0.0f);
        PlayerParty.getPartyMember(1).transform.position = new Vector3(-6.0f, 0.0f, 0.0f);
        PlayerParty.getPartyMember(2).transform.position = new Vector3(-4.0f, 0.0f, 0.0f);
        PlayerParty.getPartyMember(3).transform.position = new Vector3(-2.0f, 0.0f, 0.0f);

        //Restore hp of dead characters
        if (!PlayerParty.getPartyMember(0).GetComponent<PlayableCharacter>().isAlive())
            PlayerParty.getPartyMember(0).GetComponent<PlayableCharacter>().reviveCharacterForNextBattle();
        if (!PlayerParty.getPartyMember(1).GetComponent<PlayableCharacter>().isAlive())
            PlayerParty.getPartyMember(1).GetComponent<PlayableCharacter>().reviveCharacterForNextBattle();
        if (!PlayerParty.getPartyMember(2).GetComponent<PlayableCharacter>().isAlive())
            PlayerParty.getPartyMember(2).GetComponent<PlayableCharacter>().reviveCharacterForNextBattle();
        if (!PlayerParty.getPartyMember(3).GetComponent<PlayableCharacter>().isAlive())
            PlayerParty.getPartyMember(3).GetComponent<PlayableCharacter>().reviveCharacterForNextBattle();

        EnemyEncounter.LoadEncounter();
                
        //--------------------------------------------------------------------------------------------------------------
    }
    
    public void Start()
    {
        Round = 1;
        isBattling = true;
        
        //Load Background
        
        //Go Through all Items in PlayerParty and add buffs to BattleLogicHandler
        //Reset armor and resolve to 0
        //Reset cooldown
        foreach (GameObject C in PlayerParty.GetLivingPartyMembers())
        {
            PlayableCharacter P = C.GetComponent<PlayableCharacter>();
            foreach (GameItem I in P.Inventory)
            {
                I.OnApply();
            }
            P.setResolve(0);
            P.setCurrentArmor(0);
            P.ResetAllCooldown();
            P.setHasCasted(false);
        }

        
        //Begin Combat Encounter
        StartCoroutine(TurnOrder());
        
    }
    
    IEnumerator TurnOrder()
    {
        
        while (isBattling)
        {
            //Start the Turn
            BattleLogicHandler.BeginRound(Round);
            
            //Create new moves if needed
            EM_Handler.GenerateMoves();
            
            //Player turn
            Debug.Log("PlayerTurn");
            BT_Indicator.FlashTurn(true);
            BattleLogicHandler.PlayerPreTurn();
            
            BattleLogicHandler.CheckForAllPlayersDeaths();
            BattleLogicHandler.CheckForEncounterDeath();
            
            AB_Handler.StartCastingMode();
            
            while (AB_Handler.IsInCastingMode())
            {
                yield return null;
            }
            
            //Fix the way game ends. Current issues:
            //Player has to hit end turn button even if all enemies are death
            //Enemy plays out remaining attack
            //Check Players Death first, then enemies
            Debug.Log("Checking deaths");
            
            BattleLogicHandler.PlayerPostTurn();
            
            Debug.Log("EnemyTurn");
            BT_Indicator.FlashTurn(false);
            BattleLogicHandler.EnemyPreTurn();
            
            BattleLogicHandler.CheckForAllPlayersDeaths();
            BattleLogicHandler.CheckForEncounterDeath();
            
            EM_Handler.BeginEnemyTurn();
            while (EM_Handler.EnemyisMoving)
            {
                yield return null;
            }
            
            //Check Players Death first, then enemies
            Debug.Log("Checking deaths");
            BattleLogicHandler.CheckForAllPlayersDeaths();
            BattleLogicHandler.CheckForEncounterDeath();
            
            BattleLogicHandler.EnemyPostTurn();
            
            BattleLogicHandler.EndCombatRound(Round);


            
            
                
            Round+=1;
        }
        
        
    }
}
