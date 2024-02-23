using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using UnityEngine.UI;
using ItemUtil;

public class BattleSceneHandler : MonoBehaviour
{
    
    public GameObject E1;
    public GameObject E2;
    
    public static int Round;
    private bool isBattling;
    
    public AbilityButtonHandler AB_Handler;
    
    public EnemyMoveHandler EM_Handler;
    
    public HealthArmorHandler HA_Handler;
    
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

        if (PlayerParty.IsPartyDead())
        {
            //Changes the End Turn button to move to next battle scene
            AB_Handler.NextTurnButton.gameObject.GetComponent<NextTurnButtonScript>().EditButtonFunction("ReturnToItemScene");
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
                
        //Place Player in correct location
        if (PlayerParty.getPartyMember(0).GetComponent<Character>().isAlive())
            PlayerParty.getPartyMember(0).transform.position = new Vector3(-8.0f,0.0f,0.0f);
        if (PlayerParty.getPartyMember(1).GetComponent<Character>().isAlive())
            PlayerParty.getPartyMember(1).transform.position = new Vector3(-6.0f,0.0f,0.0f);
        if (PlayerParty.getPartyMember(2).GetComponent<Character>().isAlive())
            PlayerParty.getPartyMember(2).transform.position = new Vector3(-4.0f,0.0f,0.0f);
        if (PlayerParty.getPartyMember(3).GetComponent<Character>().isAlive())
            PlayerParty.getPartyMember(3).transform.position = new Vector3(-2.0f,0.0f,0.0f);
        
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
