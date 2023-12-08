using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

public class EnemyEncounterHandler : MonoBehaviour
{
    
    public int EncounterSize;
    
    public GameObject[] InitialSpawnPool;
    
    public Vector2[] SpawnLocation;
    
    public GameObject[] SpawnPool;
    
    public GameObject CreateEnemy(int Pool, int Location, bool Initial)
    {
        GameObject G;
        if (Initial)
            G = Instantiate(InitialSpawnPool[Pool], SpawnLocation[Location], Quaternion.identity) as GameObject;
        else
            G = Instantiate(SpawnPool[Pool], SpawnLocation[Location], Quaternion.identity) as GameObject;
        
        EnemyCharacter E = G.GetComponent<EnemyCharacter>();
        E.InitialBuffs();
        return G;
    }
    
}
