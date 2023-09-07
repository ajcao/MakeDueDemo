using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneHandler : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;
    public GameObject P5;
    public GameObject P6;
    public GameObject P7;
    public GameObject P8;
    
    public GameObject E1;
    public GameObject E2;
    
    public static int Turn;
    private bool isBattling;
    
    public AbilityButtonHandler AB_Handler;
    
    public EnemyMoveHandler EM_Handler;
    
    public HealthArmorHandler HA_Handler;
    
    public delegate void EndGameDelegate();
    public static EndGameDelegate EndGame;
    
    public static int GetTurn()
    {
        return Turn;
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
        
        //TEMP CODE TO CREATE PLAYER PARTY------------------------------------------------------------------------------
        //WILL BE HANDLED BY SYSTEM TO CREATE PARTY
        //Later "Add Don't Destroy on Load"
        
        //Get random characters
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
        
        int i = 0;
        while (TotalCharacterArray.Count > 0 && i < 4)
        {
            int r = Random.Range(0, TotalCharacterArray.Count);
            GameObject P = TotalCharacterArray[r];
            CurrentCharacterArray[i] = P;
            TotalCharacterArray.Remove(P);
            i++;
        }
        
        
        
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[0], new Vector2(-8,0), Quaternion.identity) as GameObject));
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[1], new Vector2(-5,0), Quaternion.identity) as GameObject));
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[2], new Vector2(-2,0), Quaternion.identity) as GameObject));
        PlayerParty.AddPartyMember((Instantiate(CurrentCharacterArray[3], new Vector2(1,0), Quaternion.identity) as GameObject));
        
        EnemyEncounter.AddEncounterMember((Instantiate(E1, new Vector2(6,0), Quaternion.identity) as GameObject));
        EnemyEncounter.AddEncounterMember((Instantiate(E2, new Vector2(9,0), Quaternion.identity) as GameObject));
                
        //--------------------------------------------------------------------------------------------------------------
    }
    
    public void Start()
    {
        Turn = 1;
        isBattling = true;
        
        BattleLogicHandler.Init();
        
        //Load Background
        
        //Go Through all Items in PlayerParty and add buffs to BattleLogicHandler
        
        StartCoroutine(TurnOrder());
        
    }
    
    IEnumerator TurnOrder()
    {
        
        while (isBattling)
        {
            //Start the Turn
            BattleLogicHandler.BeginRound(Turn);
            
            //Check enemy moves, create new moves if needed
            EM_Handler.DisplayMoves();
            
            //Player turn
            Debug.Log("PlayerTurn");
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
            
            EM_Handler.ResetEnemyStamina();
            
            AB_Handler.ResetPlayerTurn();
            
            BattleLogicHandler.EndCombatRound();
            
            

            
            
                
            Turn+=1;
        }
        
        
    }
}
