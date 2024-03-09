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
    
    private Coroutine Flashing;
    
    //Determines what second bar is
    private bool IsResolveBar;
    
    
    public void Init(Character inputC)
    {
        C = inputC;
        
        IsResolveBar = (C.GetType()).IsSubclassOf(typeof(PlayableCharacter));
        
        if (IsResolveBar)
            SecondBar.color = Color.blue;
        else
            SecondBar.color = new Color(0.0f, 0.118f, 0.118f);
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
            
            //Flash if player has max resolve
            if ((P.getResolve() == P.getMaxResolve()) && (Flashing == null))
            {
                Flashing = StartCoroutine(FlashingBar());
            }
            else if ((P.getResolve() != P.getMaxResolve()) && (Flashing != null))
            {
                SecondBar.color = Color.blue;
                StopCoroutine(Flashing);
                Flashing = null;
            }
        }
        //Else display player resolve bar
        else
        {
            EnemyCharacter E = (EnemyCharacter) C;
            SecondBarText.text = "" + E.getPoise() + "/" + E.getMaxPoise();
            SecondBar.transform.localScale = new Vector3( (E.getPoise() / (float) E.getMaxPoise()), 1.0f, 1.0f);
            
            //Flash if enemy can regenerate poise
            if (E.canPoiseRegenerate && (E.getPoise() != E.getMaxPoise()) && (Flashing == null))
            {
                Flashing = StartCoroutine(FlashingBar());
            }
            else if ((!E.canPoiseRegenerate || (E.getPoise() == E.getMaxPoise())) && (Flashing != null))
            {
                SecondBar.color = new Color(0.0f, 0.118f, 0.118f);
                StopCoroutine(Flashing);
                Flashing = null;
            }
        }
        
    }
    
    IEnumerator FlashingBar()
    {
        while (true)
        {
            for (int i = 0; i < 60; i++)
            {
                SecondBar.color += new Color(0.01f, 0.01f, 0.0f);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.1f);
            
            for (int i = 0; i < 60; i++)
            {
                SecondBar.color -= new Color(0.01f, 0.01f, 0.0f);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}
