using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
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
        if (PlayerParty.IsPartyDead())
        {
            Debug.Log("YOU LOSE!");
        }
        else
        {
            Debug.Log("YOU WIN!");
        }
    }
    
    public void Awake()
    {
        //Add method to delegate for ending the game
        EndGame = EndGameMethod;
        
        //Place Player in correct location
        PlayerParty.getPartyMember(0).transform.position = new Vector3(-9.0f,0.0f,0.0f);
        PlayerParty.getPartyMember(1).transform.position = new Vector3(-6.5f,0.0f,0.0f);
        PlayerParty.getPartyMember(2).transform.position = new Vector3(-4.0f,0.0f,0.0f);
        PlayerParty.getPartyMember(3).transform.position = new Vector3(-1.5f,0.0f,0.0f);
        
        EnemyEncounter.LoadEncounter();
                
        //--------------------------------------------------------------------------------------------------------------
    }
    
    public void Start()
    {
        Round = 1;
        isBattling = true;
        
        BattleLogicHandler.Init();
        
        //Load Background
        
        //Go Through all Items in PlayerParty and add buffs to BattleLogicHandler
        foreach (GameObject C in PlayerParty.GetLivingPartyMembers())
        {
            PlayableCharacter P = C.GetComponent<PlayableCharacter>();
            foreach (GameItem I in P.Inventory)
            {
                I.OnApply();
            }
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
            BattleLogicHandler.CheckForAllPlayersDeaths();
            BattleLogicHandler.CheckForEncounterDeath();
            
            BattleLogicHandler.PlayerPostTurn();
            
            
            Debug.Log("EnemyTurn");
            
            BattleLogicHandler.EnemyPreTurn();
            
            
            
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
