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
    
    public bool CanCastOnDead = false;
    
    
    
    public abstract void onCast(Character C);
    
    public void onCastWrapper()
    {
        
        foreach (Character C in this.TargetArray)
        {
            if (EC.isAlive() && (C.isAlive() || this.CanCastOnDead))
                this.onCast(C);
        }
    }
    
    public abstract string MoveIndicatorText();
    
    
    
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
    
    public abstract void AdditionalMoveDeletion();
    
    public void DeleteMoveIndicator()
    {
        //Since EnemyMoves and EnemyMoveIndicator are closely linked
        //Properly set null references as "garbage" collection
        GameObject.Destroy(MoveIndicator);
        MoveIndicator = null;
        
        this.AdditionalMoveDeletion();
    }
    
    public abstract string getAnimation();
    
    

}

}
