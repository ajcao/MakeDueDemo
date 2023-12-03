using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounterHandler : MonoBehaviour
{
    
    public int EncounterSize;
    
    public GameObject[] InitialSpawnPool;
    
    public Vector2[] SpawnLocation;
    
    public GameObject[] SpawnPool;
    
    public GameObject CreateEnemy(int Pool, int Location, bool Initial)
    {
        if (Initial)
            return Instantiate(InitialSpawnPool[Pool], SpawnLocation[Location], Quaternion.identity) as GameObject;
        else
            return Instantiate(SpawnPool[Pool], SpawnLocation[Location], Quaternion.identity) as GameObject;
    }
    
}
