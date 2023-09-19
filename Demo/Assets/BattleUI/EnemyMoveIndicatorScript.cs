using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using TMPro;
using UnityEngine.UI;
using CharacterUtil;
using UnityEngine.EventSystems;

public class EnemyMoveIndicatorScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    EnemyMove EM;
    
    protected int MaxIndicatorAmount = 4;
    
    public SpriteRenderer[] Target;
    
    public TextMeshPro text;
    
    private Sprite Empty;
    
    public bool Condensed;
    
    
    public void Init(EnemyMove InputEM)
    {
        EM = InputEM;
        
        SpriteRenderer S = this.gameObject.transform.Find("EnemyMoveSprite").gameObject.GetComponent<SpriteRenderer>();
        S.sprite = EM.getIcon();
        
        Empty = Resources.Load<Sprite>("EnemyCharacterImages/Blank");
    }
    
    //Used to condensed moves
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        bool newState = !Condensed;
        foreach (EnemyMove E in EM.EC.getCurrentMoves())
        {
            EnemyMoveIndicatorScript EScript = E.getMoveIndicator().GetComponent<EnemyMoveIndicatorScript>();
            EScript.Condensed = newState;
        }
        Condensed = newState;
        GameObject.Find("EnemyMoveHandlerGameObject").GetComponent<EnemyMoveHandler>().DrawMoves(this.EM.EC);
    }
    
    //Used to highlight targets
    public void OnPointerEnter(PointerEventData eventData)
	{
        foreach (Character C in EM.getTargetArray())
        {
            if ((C.GetType()).IsSubclassOf(typeof(PlayableCharacter)))
            {
                C.gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
            }
            
            if ((C.GetType()).IsSubclassOf(typeof(EnemyCharacter)))
            {
                C.gameObject.GetComponent<SpriteRenderer>().material.color = Color.blue;
            }
            
        }
	}
    
    public void OnPointerExit(PointerEventData eventData)
	{
        foreach (Character C in EM.getTargetArray())
        {
            C.gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        }
	}
    
    public void Clean()
    {
        EM = null;
        Destroy(this.gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        text.text = EM.MoveIndicatorText();
        this.DisplayTargetArray();
        
        
    }
    
    private void DisplayTargetArray()
    {
        //Maps Target Array to Slots
        foreach (Character C in EM.getTargetArray())
        {
            foreach (SpriteRenderer I in Target)
            {
                //Null image means put the current target character in the slot
                if (I.sprite == Empty)
                {
                    I.sprite = C.getCharacterIcon();
                    break;
                }
                
                //Else if the same target character is already represented, move onto next character in target array
                if (I.sprite == C.getCharacterIcon())
                {
                    break;
                }
            }
        }
        
        //Scans through and makes slots visibile or invisibile
        //This would also be the section to handle greying out targets that are dead
        foreach (SpriteRenderer I in Target)
        {
            if (I.sprite == Empty)
            {
                I.gameObject.SetActive(false);
            }
            else
            {
                I.gameObject.SetActive(true); 
            }

        }
        
        
    }
}
