using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using TMPro;
using EnemyTargetingLibraryUtil;

namespace EnemyMoveUtil
{
    
public class EnemyDefendMove : EnemyMove
{
    int defendAmount;
    
    public EnemyDefendMove(EnemyCharacter inputC, int d, Character[] CArray)
    {
        EC = inputC;
        TargetArray = CArray;
        defendAmount = d;
        AbilityIcon = Resources.Load<Sprite>("AbilityImages/DefendIcon");
        
    }
    
    public override void onCast(Character C)
    {
        BattleLogicHandler.GainArmor(C, C, defendAmount + EC.getDefenseOutputModifier());
    }
    
    public override void AdditionalMoveDeletion()
    {
        return;
    }
    
    public override string MoveIndicatorText()
    {
        return "" + (defendAmount + EC.getDefenseOutputModifier());
    }
    
    public override string getAnimation()
    {
        return "Jump";
    }
}

}