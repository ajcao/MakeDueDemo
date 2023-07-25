using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

public class DefenseResolveHandler : MonoBehaviour
{
    public GameObject DefenseResolvePrefab;
    
    void Start()
    {
        for (int i = 0; i < PlayerParty.getPartySize(); i++)
        {
            GameObject C = PlayerParty.getPartyMember(i);
            GameObject DefenseResolve = Instantiate(DefenseResolvePrefab, C.transform.position + new Vector3(0f,1f,0f), Quaternion.identity, C.transform) as GameObject;
            DefenseResolve.GetComponent<DefenseResolveScript>().Init(C.GetComponent<PlayableCharacter>());
        }
    }

}
