using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using TMPro;
using UnityEngine.UI;
using CharacterUtil;

public class EnemyMoveIndicatorScript : MonoBehaviour
{
    EnemyMove EM;
    
    protected int MaxIndicatorAmount = 4;
    
    public SpriteRenderer[] Target;
    
    public TextMeshPro text;
    
    private Sprite Empty;
    
    
    public void Init(EnemyMove InputEM)
    {
        EM = InputEM;
        this.gameObject.transform.Find("EnemyMoveSprite").gameObject.GetComponent<SpriteRenderer>().sprite = EM.getIcon();
        
        Empty = Resources.Load<Sprite>("EnemyCharacterImages/Blank");
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
