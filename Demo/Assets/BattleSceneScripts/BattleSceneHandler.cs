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
        //Add method to delegate
        EndGame = EndGameMethod;
        
        //Temporary??
        PlayerParty.getPartyMember(0).transform.position = new Vector3(-7.5f,0.0f,0.0f);
        PlayerParty.getPartyMember(1).transform.position = new Vector3(-5.0f,0.0f,0.0f);
        PlayerParty.getPartyMember(2).transform.position = new Vector3(-2.5f,0.0f,0.0f);
        PlayerParty.getPartyMember(3).transform.position = new Vector3(0.0f,0.0f,0.0f);
        
        EnemyEncounter.AddEncounterMember((Instantiate(E1, new Vector2(5.0f,0.0f), Quaternion.identity) as GameObject));
        EnemyEncounter.AddEncounterMember((Instantiate(E2, new Vector2(7.5f,0.0f), Quaternion.identity) as GameObject));
                
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
        
        StartCoroutine(TurnOrder());
        
    }
    
    IEnumerator TurnOrder()
    {
        
        while (isBattling)
        {
            //Start the Turn
            BattleLogicHandler.BeginRound(Round);
            
            //Check enemy moves, create new moves if needed
            EM_Handler.DisplayMoves();
            
            //Player turn
            Debug.Log("PlayerTurn");
            BattleLogicHandler.PlayerPreTurn();
            
            AB_Handler.StartCastingMode();
            
            while (AB_Handler.IsInCastingMode())
            {
                yield return null;
            }
            
            BattleLogicHandler.CheckForEncounterDeath();
            
            
            Debug.Log("EnemyTurn");
            
            BattleLogicHandler.EnemyPreTurn();
            
            
            
            EM_Handler.BeginEnemyTurn();
            while (EM_Handler.EnemyisMoving)
            {
                yield return null;
            }
            
            BattleLogicHandler.CheckForEncounterDeath();
            
            BattleLogicHandler.EndCombatRound(Round);
            
            

            
            
                
            Round+=1;
        }
        
        
    }
}
