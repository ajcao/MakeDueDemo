using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneHandler : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;
    
    public GameObject E1;
    public GameObject E2;
    
    private int Turn;
    private bool isBattling;
    
    public AbilityButtonHandler AB_Handler;
    
    public EnemyMoveHandler EM_Handler;
    
    public HealthArmorHandler HA_Handler;
    
    
    public void Awake()
    {
        
        //TEMP CODE TO CREATE PLAYER PARTY------------------------------------------------------------------------------
        //WILL BE HANDLED BY SYSTEM TO CREATE PARTY
        //Later Add Don't Destroy on Load
        
        
        PlayerParty.setPartyMember((Instantiate(P1, new Vector2(-8,0), Quaternion.identity) as GameObject), 0);
        PlayerParty.setPartyMember((Instantiate(P2, new Vector2(-5,0), Quaternion.identity) as GameObject), 1);
        PlayerParty.setPartyMember((Instantiate(P3, new Vector2(-2,0), Quaternion.identity) as GameObject), 2);
        PlayerParty.setPartyMember((Instantiate(P4, new Vector2(1,0), Quaternion.identity) as GameObject), 3);
        
        EnemyEncounter.createNewEncounter(2);
        EnemyEncounter.setEncounterMember((Instantiate(E1, new Vector2(6,0), Quaternion.identity) as GameObject),0);
        EnemyEncounter.setEncounterMember((Instantiate(E2, new Vector2(9,0), Quaternion.identity) as GameObject),1);
                
        //--------------------------------------------------------------------------------------------------------------
    }
    
    public void Start()
    {
        Turn = 1;
        isBattling = true;
        
        BattleLogicHandler.Init();
        
        //Reset everyone's cast
        AB_Handler.ResetEveryoneCast();
        
        //Load Background
        
        //Go Through all Items in PlayerParty and add buffs to BattleLogicHandler
        
        StartCoroutine(TurnOrder());
        
    }
    
    IEnumerator TurnOrder()
    {
        
        while (isBattling)
        {
            //Check enemy moves, create new moves if needed
            EM_Handler.DisplayMoves();
            
            //Trigger start of turn events (items included)
            BattleLogicHandler.PlayerPreTurn();
            
            //Player turn
            Debug.Log("PlayerTurn");
            AB_Handler.StartCastingMode();
            while (!AB_Handler.EveryoneHasCasted())
            {
                yield return null;
            }
            Debug.Log("EnemyTurn");
            yield return new WaitForSeconds(2);
            
            BattleLogicHandler.EnemyPreTurn();
            
            EM_Handler.BeginEnemyTurn();
            
            EM_Handler.ResetEnemyCast();
            
            AB_Handler.ResetEveryoneCast();
            
            BattleLogicHandler.EndCombatRound();
            
            

            
            
                
            Turn+=1;
        }
        
        
        
    }
}
