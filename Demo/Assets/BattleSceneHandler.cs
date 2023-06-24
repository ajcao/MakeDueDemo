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
        EnemyEncounter.setEncounterMember((Instantiate(E1, new Vector2(9,0), Quaternion.identity) as GameObject),1);
                
        
        //--------------------------------------------------------------------------------------------------------------
    }
    
    public void Start()
    {
        
        BattleLogicHandler.Init();
        
        //Health Bar Handler is init indepdently
        HealthBarHandler HB_Handler = GameObject.Find("HealthBarHandlerGameObject").GetComponent<HealthBarHandler>();
        
        //AbilityButtonhandler is init indepently
        AbilityButtonHandler AB_Handler = GameObject.Find("AbilityButtonHandlerGameObject").GetComponent<AbilityButtonHandler>();
        
        //Temp to test out if Ability/Targeting works
        AB_Handler.StartCastingMode();
        
        //Load Background
        
        //Go Through all Items in PlayerParty and add buffs to BattleLogicHandler
        
        //Go Through all Enemies and update their MovesUI to display their current movepool
        
    }
}
