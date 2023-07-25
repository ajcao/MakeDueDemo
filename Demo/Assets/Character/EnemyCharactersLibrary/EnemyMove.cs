using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using TMPro;

namespace EnemyMoveUtil
{
    
public abstract class EnemyMove
{
    //Ability Owner
    public EnemyCharacter EC;
    
    //Enemies are capable of casting abilities on multiple targets
    protected Character[] TargetArray;
    
    protected GameObject MoveIndicator;
    
    
    
    public abstract void onCast();
    
    public abstract string MoveIndicatorText();
    
    public abstract void MoveTargetIndicatorText(GameObject Canvas, Character[] Target);
    
    
    
    //Speical moves are not lost during stunned turns
    public bool Special;
    
    protected Sprite AbilityIcon;
    
    public void AssignIndicator(GameObject MI)
    {
        MoveIndicator = MI;
    }
    
    public GameObject getMoveIndicator()
    {
        return MoveIndicator;
    }
    
    public Sprite getIcon()
    {
        return AbilityIcon;
    }
    
    public Character[] getTargetArray()
    {
        return TargetArray;
    }
    
    public bool IsSpecial()
    {
        return Special;
    }
    
    public void DeleteMove()
    {
        //Since EnemyMoves and EnemyMoveIndicator are closely linked
        //Properly set null references as "garbage" collection
        MoveIndicator.GetComponent<EnemyMoveIndicatorScript>().Clean();
        MoveIndicator = null;
    }
    
    

}

}
