using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterUtil;

public class HealthBarHandler : MonoBehaviour
{
    public GameObject HealthPrefab;
    
    
    //Currently HealthBar operates independently of BattleSceneHandler
    //May need to add way for BattleSceneHandler to initalize HealthBarhandler
    public void Start()
    {
        //Player HealthBar
        for (int i = 0; i < PlayerParty.getPartySize(); i++)
        {
            GameObject C = PlayerParty.getPartyMember(i);
            GameObject HealthBar = Instantiate(HealthPrefab, C.transform.position + new Vector3(0f,-1.5f,0f), Quaternion.identity, C.transform) as GameObject;
            HealthBar.GetComponent<HealthBarScript>().Init(C.GetComponent<Character>());
        }
        
        //Enemy HealthBar
        for (int i = 0; i < EnemyEncounter.getEncounterSize(); i++)
        {
            GameObject C = EnemyEncounter.getEncounterMember(i);
            GameObject HealthBar = Instantiate(HealthPrefab, C.transform.position + new Vector3(0f,-1.5f,0f), Quaternion.identity, C.transform) as GameObject;
            HealthBar.GetComponent<HealthBarScript>().Init(C.GetComponent<Character>());
        }
        

    }

    
}