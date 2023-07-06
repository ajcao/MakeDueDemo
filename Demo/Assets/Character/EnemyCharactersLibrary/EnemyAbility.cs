using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace EnemyAbilityUtil
{
    
public abstract class EnemyAbility
{
        //Ability Owner
    public EnemyCharacter EC;
    
    //Enemies are capable of casting abilities on multiple targets
    protected Character[] TargetArray;
    
    public abstract void onCast(Character C);
    
    
    
    
    //Speical moves are not lost during stunned turns
    public bool Special;
    
    protected Sprite AbilityIcon;

}

}
