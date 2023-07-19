using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;

public class EnemyMoveHandler : MonoBehaviour
{
    public bool EnemyHasMoved;
    
    public GameObject EnemyMoveIndicatorPrefab;
    
    //Function also handles creating new moves too
    public void DisplayMoves()
    {
        foreach (GameObject G in EnemyEncounter.getEncounter())
        {
            EnemyCharacter E = G.GetComponent<EnemyCharacter>();
            //If the enemy has no moves, get new moves
            //Then display the new moves
            if (E.getCurrentMoves().Count == 0)
            {
                E.GenerateMoves();
                int i = 1;
                foreach (EnemyMove EM in E.getCurrentMoves())
                {
                    GameObject EM_Indicator = Instantiate(EnemyMoveIndicatorPrefab, E.transform.position + new Vector3(0.0f,i*1.5f,0.0f), Quaternion.identity, E.transform) as GameObject;
                    EM.AssignIndicator(EM_Indicator);
                    EM_Indicator.GetComponent<EnemyMoveIndicatorScript>().Init(EM);
                    
                    i+=1;
                }
            }
            //Else rearrange the old moves
            else
            {
                int i = 1;
                foreach (EnemyMove EM in E.getCurrentMoves())
                {
                    GameObject EM_Indicator = EM.getMoveIndicator();
                    EM_Indicator.transform.position = E.transform.position + new Vector3(0.0f,i*1.5f,0.0f);
                    
                    i+=1;
                }
            }
        }
    }
    
    public void BeginEnemyTurn()
    {
        foreach (GameObject G in EnemyEncounter.getEncounter())
        {
            EnemyCharacter E = G.GetComponent<EnemyCharacter>();
            E.EnemyCastMoves();
            
        }
    }

}
