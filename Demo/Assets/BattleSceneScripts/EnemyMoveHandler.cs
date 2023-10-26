using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using EnemyMoveUtil;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class EnemyMoveHandler : MonoBehaviour
{
    public bool EnemyisMoving;
    
    public GameObject EnemyMoveIndicatorPrefab;
    
    public NextTurnButtonScript NextTurnButton;
    
    public BattleAnimationHandler BattleAnimation;
    
    //Function also handles creating new moves too
    public void DisplayMoves()
    {
        foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
        {
            EnemyCharacter E = G.GetComponent<EnemyCharacter>();
            //If the enemy has no moves, get new moves
            //Then display the new moves
            if (E.getCurrentMoves().Count == 0)
            {
                E.GenerateMoves();
            }
        }
    }
    
    public void Update()
    {
        foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
        {
            EnemyCharacter E = G.GetComponent<EnemyCharacter>();
            this.DrawMoves(E);
        }
    }
    
    public void DrawMoves(EnemyCharacter E)
    {
        int i = 0;
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
            //If the move is condensed
            if (EM_Indicator.GetComponent<EnemyMoveIndicatorScript>().Condensed == true)
            {                
                EM_Indicator.transform.position = E.transform.position + new Vector3(0.0f,1.5f+i*0.2f,0.0f);
                //Have the first hitbox be normal size
                if (i == 0)
                {
                    EM_Indicator.GetComponent<BoxCollider2D>().size = new Vector2(1.47f, 1.04f);
                    EM_Indicator.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);                   
                }
                else
                {
                    EM_Indicator.GetComponent<BoxCollider2D>().size = new Vector2(1.47f, 0.21f);
                    EM_Indicator.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.4f);
                }
            }
            else
            {
                EM_Indicator.transform.position = E.transform.position + new Vector3(0.0f,1.5f+i*1.5f,0.0f);
                EM_Indicator.GetComponent<BoxCollider2D>().size = new Vector2(1.47f, 1.04f);
                EM_Indicator.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);
            }
            EM_Indicator.GetComponent<SortingGroup>().sortingOrder = -5*i;
            
            i+=1;
        }
    }
    
    public void BeginEnemyTurn()
    {
        EnemyisMoving = true;
        StartCoroutine(EnemyTurn());
    }
    
    IEnumerator EnemyTurn()
    {
        //Fix to avoid using size
        //Relies on fixing Player Party and Enemy Encounter
        Sprite ButtonImage = Resources.Load<Sprite>("EnemyTurnButton") as Sprite;
        NextTurnButton.gameObject.GetComponent<Image>().sprite = ButtonImage;
        foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
        {
            EnemyCharacter E = G.GetComponent<EnemyCharacter>();
            
            Debug.Log("Popping");
            EnemyMove EM = E.getCurrentMoves().Peek();
            BattleAnimation.StartAnimation(EM.getMoveIndicator().transform.GetChild(0).gameObject, "Flash");
            while (BattleAnimation.isAnimationPlaying())
            {
                yield return null;
            }            
            EM = E.getCurrentMoves().Pop();
            EM.DeleteMoveIndicator();
            
            Debug.Log("Casting");
            BattleAnimation.StartAnimation(G, EM.getAnimation());
            while (BattleAnimation.isAnimationPlaying())
            {
                yield return null;
            }
            EM.onCastWrapper();
            
        }
        EnemyisMoving = false;
        
    }

}
