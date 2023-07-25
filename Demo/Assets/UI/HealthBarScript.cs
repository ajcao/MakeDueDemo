using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CharacterUtil;

public class HealthBarScript : MonoBehaviour
{
    public TextMeshPro textmeshPro;
    public Character C;
    
    //Handles hiding the health bar for dead characters
    private float defaultHeight;
    private float hiddenHeight;
    
    public void Init(Character inputC)
    {
        textmeshPro = this.gameObject.GetComponent<TextMeshPro>();
        C = inputC;
        textmeshPro.fontSize = 1;
        defaultHeight = this.transform.position.y;
        hiddenHeight = defaultHeight * 5;
    }
    
    
    public void Update()
    {
        //If enemy display Stamina/Stun bar
        string SecondStatString;
        if ( (C.GetType()).IsSubclassOf( typeof(EnemyCharacter) ) )
        {
            EnemyCharacter E = (EnemyCharacter) C;
            SecondStatString = "    " + E.getStamina() + "/" + E.getMaxStamina();
        }
        //Else display player resolve bar
        else
        {
            PlayableCharacter P = (PlayableCharacter) C;
            SecondStatString = "    " + P.getResolve() + "/" + P.getMaxResolve();
        }
        textmeshPro.text = "(" + C.getCurrentArmor() + ")" + C.getCurrentHealth() + "/" + C.getMaxHealth() + "\n" + SecondStatString;
        
        //Hides/reveal health bar depending on health
        if (C.getCurrentHealth() <= 0 && this.transform.position.y != hiddenHeight)
        {
            this.transform.position = new Vector3(this.transform.position.x, hiddenHeight, this.transform.position.z);
        }
        else if (C.getCurrentHealth() > 0 && this.transform.position.y != defaultHeight)
        {
            this.transform.position = new Vector3(this.transform.position.x, defaultHeight, this.transform.position.z);
        }
    }

}
