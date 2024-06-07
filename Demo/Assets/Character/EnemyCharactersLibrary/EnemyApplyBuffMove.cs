using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;
using EnemyTargetingLibraryUtil;

namespace EnemyMoveUtil
{
    
public class EnemyApplyBuffMove : EnemyMove
{
    List<Buff> BuffList;
    int? Intensity;
    int? Duration;
    
    public EnemyApplyBuffMove (EnemyCharacter InputC, Character[] CArray, List<Buff> BN)
    {
        TargetArray = CArray;
        EC = InputC;
        BuffList = BN;
        if ((CArray[0].GetType()).IsSubclassOf(typeof(EnemyCharacter)))
        {
            AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericBuff");
        }
        else
        {
            AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericDebuff");
        }
    }

    public override void onCast(Character C)
    {
        foreach (Buff b in BuffList)
        {
            if (b.getBuffTarget() == C)
            {
                BattleLogicHandler.OnBuffApply(b);
            }
        }
    }

    public override void AdditionalMoveDeletion()
    {
        return;
    }
    
    public override string MoveIndicatorText()
    {
        if ((TargetArray[0].GetType()).IsSubclassOf(typeof(EnemyCharacter)))
        {
            return "Buff";
        }
        else
        {
            return "Dbf";
        }
    }
    
    public override string getAnimation()
    {
        return "Jump";
    }
}

}
