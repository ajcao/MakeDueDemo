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
        foreach (GameObject C in PlayerParty.GetLivingPartyMembers())
        {
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
        foreach (GameObject C in EnemyEncounter.GetLivingEncounterMembers())
        {
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
    
    private Vector3 GetNextBuffLocation(int NthBuff)
    {
        int MaxBuffRow = 4;
        float xCoord = -0.75f + Mathf.Repeat(NthBuff * 0.5f,2.0f);
        float yCoord = -2.0f + -0.5f*(NthBuff / MaxBuffRow);
        return new Vector3(xCoord, yCoord, 0f);
    }
}
