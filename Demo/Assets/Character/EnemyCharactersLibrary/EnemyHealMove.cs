using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using TMPro;
using EnemyTargetingLibraryUtil;

namespace EnemyMoveUtil
{
    
public class EnemyHealMove : EnemyMove
{
    int healAmount;
    
    public EnemyHealMove(EnemyCharacter inputC, int d, Character[] CArray)
    {
        EC = inputC;
        TargetArray = CArray;
        healAmount = d;
        AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericAbilityHeal");
        
    }
    
    public override void onCast(Character C)
    {
        BattleLogicHandler.GainHealth(C, healAmount);
    }
    
    public override void AdditionalMoveDeletion()
    {
        return;
    }
    
    public override string MoveIndicatorText()
    {
        return "" + (healAmount + EC.getDefenseOutputModifier());
    }
    
    public override string getAnimation()
    {
        return "Jump";
    }
}

}