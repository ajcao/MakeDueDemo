using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace EnemyMoveUtil
{
    
public class EnemyApplyBuffMove : EnemyMove
{
    Buff[] BuffList;
    
    public EnemyApplyBuffMove (EnemyCharacter InputC, Buff[] BList, Character[] CArray)
    {
        TargetArray = CArray;
        Special = false;
        BuffList = BList;
        EC = InputC;
        AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericBuff");
    }

    public override void onCast()
    {
        foreach (Buff B in BuffList)
        {
            BattleLogicHandler.OnBuffApply(B);
        }
    }
    
    public override string MoveIndicatorText()
    {
        return "+" + BuffList[0].getIntensity();
    }
}

}
