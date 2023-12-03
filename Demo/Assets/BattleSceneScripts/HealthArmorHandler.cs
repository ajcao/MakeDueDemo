using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterUtil;

public class HealthArmorHandler : MonoBehaviour
{
    public GameObject HealthArmorPrefab;
    
    
    //Currently HealthBar operates independently of BattleSceneHandler
    //May need to add way for BattleSceneHandler to initalize HealthBarhandler
    public void Start()
    {
        //Player HealthBar
        foreach (GameObject C in PlayerParty.GetLivingPartyMembers())
        {
            GameObject HealthArmorBar = Instantiate(HealthArmorPrefab, C.transform.position + new Vector3(0f,-1.5f,0f), Quaternion.identity, C.transform) as GameObject;
            HealthArmorBar.GetComponent<HealthArmorScript>().Init(C.GetComponent<Character>());
        }
        
        //Enemy HealthBar
        foreach (GameObject C in EnemyEncounter.GetLivingEncounterMembers())
        {
            GameObject HealthArmorBar = Instantiate(HealthArmorPrefab, C.transform.position + new Vector3(0f,-1.5f,0f), Quaternion.identity, C.transform) as GameObject;
            HealthArmorBar.GetComponent<HealthArmorScript>().Init(C.GetComponent<Character>());
        }
        

    }
    
    public void AddHealthIndicator(GameObject C)
    {
        GameObject HealthArmorBar = Instantiate(HealthArmorPrefab, C.transform.position + new Vector3(0f,-1.5f,0f), Quaternion.identity, C.transform) as GameObject;
        HealthArmorBar.GetComponent<HealthArmorScript>().Init(C.GetComponent<Character>());
    }

    
}