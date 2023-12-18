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
    
    public void GenerateMoves()
    {
        foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
        {
            EnemyCharacter E = G.GetComponent<EnemyCharacter>();
            //If the enemy has no moves, get new moves
            if (E.getCurrentMoves().Count == 0)
            {
                E.GenerateMoves();
            }
        }
    }
    
    //Handles having the moves have a visual indicator
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
            
            //Get height of character sprite
            SpriteRenderer SR = E.gameObject.GetComponentInChildren<SpriteRenderer>();
            
            //If a move exist but no move indicator exists, create a move indicator
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
                
                EM_Indicator.transform.position = E.transform.position + new Vector3(0.0f,i*0.2f,0.0f) + new Vector3(0.0f, 2.0f*SR.size.y,0.0f);
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
            //Show moves as normallly
            else
            {
                EM_Indicator.transform.position = E.transform.position + new Vector3(0.0f,i*1.1f,0.0f) + new Vector3(0.0f, 2.0f*SR.size.y,0.0f);
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
        //Change the NextTurn Button to represent Enemy's turn
        Sprite ButtonImage = Resources.Load<Sprite>("EnemyTurnButton") as Sprite;
        NextTurnButton.gameObject.GetComponent<Image>().sprite = ButtonImage;
        
        foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
        {
            EnemyCharacter E = G.GetComponent<EnemyCharacter>();
            if (E.isAlive() && E.getCurrentMoves().Count > 0)
            {
                
                //Get the current move to get the animation
                EnemyMove EM = E.getCurrentMoves().Peek();
                BattleAnimation.StartAnimation(EM.getMoveIndicator().transform.GetChild(0).gameObject, "Flash");
                
                //Pause execution during animation
                while (BattleAnimation.isAnimationPlaying())
                {
                    yield return null;
                }

                //Remove the current move from the enemy's movepool
                EM = E.getCurrentMoves().Pop();
                EM.DeleteMoveIndicator();
                
                //Play the move's animation, then cast the move
                BattleAnimation.StartAnimation(G, EM.getAnimation());
                while (BattleAnimation.isAnimationPlaying())
                {
                    yield return null;
                }
                EM.onCastWrapper();
            }
            
        }
        
        //After normal moves
        //Enemies with extra lives comeback
        GameObject[] CurrentEncounter =  EnemyEncounter.getEncounter();
        for (int i = 0; i < EnemyEncounter.getEncounterSize(); i++)
        {
            if (CurrentEncounter[i] != null)
            {
                EnemyCharacter E = CurrentEncounter[i].GetComponent<EnemyCharacter>();
                if (!E.isAlive() && E.MultipleLives)
                {

                    
                    //Play the shake animation
                    BattleAnimation.StartAnimation(E.gameObject, "Shake");
                    while (BattleAnimation.isAnimationPlaying())
                    {
                        yield return null;
                    }
                    //Do not use onCastWrapper as that checks for death
                    E.EnterNextLife();
                }
            }
        }
        EnemyisMoving = false;
        
        BattleLogicHandler.CheckForAllPlayersDeaths();
        BattleLogicHandler.CheckForEncounterDeath();
        
    }

}
