using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CharacterUtil;

public class HealthArmorScript : MonoBehaviour
{
    public SpriteRenderer HealthBar;
    public SpriteRenderer SecondBar;
    public SpriteRenderer Armor;
    
    public TextMeshPro HealthText;
    public TextMeshPro SecondBarText;
    public TextMeshPro ArmorText;
    
    public Character C;
    
    //Determines what second bar is
    private bool IsResolveBar;
    
    
    public void Init(Character inputC)
    {
        C = inputC;
        
        IsResolveBar = (C.GetType()).IsSubclassOf(typeof(PlayableCharacter));
        
        if (IsResolveBar)
            SecondBar.color = Color.blue;
    }
    
    
    public void Update()
    {
        //Health
        HealthText.text = "" + C.getCurrentHealth() + "/" + C.getMaxHealth();
        HealthBar.transform.localScale = new Vector3( (C.getCurrentHealth() / (float) C.getMaxHealth()), 1.0f, 1.0f);
        
        //Armor
        if (C.getCurrentArmor() > 0)
        {
            Armor.color = Color.white;
            ArmorText.text = "" + C.getCurrentArmor(); 
        }
        else
        {
            Armor.color = Color.clear;
            ArmorText.text = "";
        }
        
        //If player display Resolve bar
        if (IsResolveBar)
        {
            PlayableCharacter P = (PlayableCharacter) C;
            SecondBarText.text = "" + P.getResolve() + "/" + P.getMaxResolve();
            SecondBar.transform.localScale = new Vector3( (P.getResolve() / (float) P.getMaxResolve()), 1.0f, 1.0f);
        }
        //Else display player resolve bar
        else
        {
            EnemyCharacter E = (EnemyCharacter) C;
            SecondBarText.text = "" + E.getStamina() + "/" + E.getMaxStamina();
            SecondBar.transform.localScale = new Vector3( (E.getStamina() / (float) E.getMaxStamina()), 1.0f, 1.0f);
        }
        
    }

}
