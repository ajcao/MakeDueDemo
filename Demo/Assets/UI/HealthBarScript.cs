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
        textmeshPro.text = "(" + C.getCurrentArmor() + ")" + C.getCurrentHealth() + "/" + C.getMaxHealth();
        textmeshPro.fontSize = 1;
        defaultHeight = this.transform.position.y;
        hiddenHeight = defaultHeight * 3;
    }
    
    public void Update()
    {
        textmeshPro.text = "(" + C.getCurrentArmor() + ")" + C.getCurrentHealth() + "/" + C.getMaxHealth();
        
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
