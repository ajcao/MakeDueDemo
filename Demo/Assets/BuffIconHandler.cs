using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffUtil;
using CharacterUtil;

public class BuffIconHandler : MonoBehaviour
{
    public GameObject BuffIconPrefab;
    
    // Update is called once per frame
    void Update()
    {
        //Player Buffs
        for (int i = 0; i < PlayerParty.getPartySize(); i++)
        {
            GameObject C = PlayerParty.getPartyMember(i);
            List<Buff> BuffList = C.GetComponent<Character>().getBuffList();
            
            //Draws the buffs
            int p = 0;
            foreach (Buff B in BuffList)
            {
                //Checks if the buff is null, then create an indicator
                if (B.GetBuffIndicator() == null)
                {
                    GameObject BuffIndicator = Instantiate(BuffIconPrefab, C.transform.position + GetNextBuffLocation(p), Quaternion.identity, C.transform) as GameObject;
                    B.AssignBuffIndicator(BuffIndicator);
                    BuffIndicator.GetComponent<BuffIconScript>().Init(B);
                }
                else
                {
                    B.GetBuffIndicator().transform.position = C.transform.position + GetNextBuffLocation(p);
                }
                p++;
            }
        }
        
        //Enemy Buffs
        for (int i = 0; i < EnemyEncounter.getEncounterSize(); i++)
        {
            GameObject C = EnemyEncounter.getEncounterMember(i);
            List<Buff> BuffList = C.GetComponent<Character>().getBuffList();
            
            //Draws the buffs
            int p = 0;
            foreach (Buff B in BuffList)
            {
                //Checks if the buff is null, then create an indicator
                if (B.GetBuffIndicator() == null)
                {
                    GameObject BuffIndicator = Instantiate(BuffIconPrefab, C.transform.position + GetNextBuffLocation(p), Quaternion.identity, C.transform) as GameObject;
                    B.AssignBuffIndicator(BuffIndicator);
                    BuffIndicator.GetComponent<BuffIconScript>().Init(B);
                }
                else
                {
                    B.GetBuffIndicator().transform.position = C.transform.position + GetNextBuffLocation(p);
                }
                p++;
            }
        }
    }
    
    private Vector3 GetNextBuffLocation(int p)
    {
        return new Vector3(-1.25f + Mathf.Repeat(p * 0.5f,3.0f), -2.1f + -0.5f*(p / 6), 0f);
    }
}
