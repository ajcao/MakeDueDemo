using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounterHandler : MonoBehaviour
{
    
    public int EncounterSize;
    
    public GameObject[] InitialSpawnPool;
    
    public Vector2[] SpawnLocation;
    
    public GameObject[] SpawnPool;
    
    public GameObject CreateEnemy(int i)
    {
        return Instantiate(InitialSpawnPool[i], SpawnLocation[i], Quaternion.identity) as GameObject;
    }
    
}
