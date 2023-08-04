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
                    GameObject EM_Indicator = Instantiate(EnemyMoveIndicatorPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, E.transform) as GameObject;
                    EM.AssignIndicator(EM_Indicator);
                    EM_Indicator.GetComponent<EnemyMoveIndicatorScript>().Init(EM);
                    
                    i+=1;
                }
            }
            DrawMoves(E);
        }
    }
    
    public void DrawMoves(EnemyCharacter E)
    {
        int i = 1;
        foreach (EnemyMove EM in E.getCurrentMoves())
        {
            GameObject EM_Indicator;
            if (EM.getMoveIndicator() == null)
            {
                EM_Indicator = Instantiate(EnemyMoveIndicatorPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, E.transform) as GameObject;
                EM.AssignIndicator(EM_Indicator);
                EM_Indicator.GetComponent<EnemyMoveIndicatorScript>().Init(EM);
            }
            EM_Indicator = EM.getMoveIndicator();
            EM_Indicator.transform.position = E.transform.position + new Vector3(0.0f,i*1.5f,0.0f);
            
            i+=1;
        }
    }
    
    public void BeginEnemyTurn()
    {    
        //Fix to avoid using size
        //Relies on fixing Player Party and Enemy Encounter
        for (int i = 0; i < EnemyEncounter.getEncounterSize(); i++)
        {
            if (i < EnemyEncounter.getEncounterSize())
            {
                EnemyCharacter E = EnemyEncounter.getEncounter()[i].GetComponent<EnemyCharacter>();
                E.EnemyCastMoves();
            }
        }
    }
    
    public void ResetEnemyStamina()
    {
        foreach (GameObject G in EnemyEncounter.getEncounter())
        {
            EnemyCharacter E = G.GetComponent<EnemyCharacter>();
            
            if (E.canStaminaRegenerate == true)
            {
                E.setStamina(Mathf.Min(E.getStamina() + E.getStaminaRegeneration(), E.getMaxStamina()));
            }
            E.canStaminaRegenerate = true;
            
            if (E.IsStunned == true)
            {
                E.IsStunned = false;
                E.setStamina(E.getMaxStamina());
            }
            
        }
    }

}
