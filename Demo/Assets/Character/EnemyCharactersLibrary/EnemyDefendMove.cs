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
        Special = false;
        defendAmount = d;
        AbilityIcon = Resources.Load<Sprite>("AbilityImages/DefendIcon");
        
    }
    
    public override void onCast()
    {
        foreach (Character C in TargetArray)
        {
            BattleLogicHandler.Armor(C, defendAmount);
        }
    }
    
    public override string MoveIndicatorText()
    {
        return "" + (defendAmount);
    }
}

}