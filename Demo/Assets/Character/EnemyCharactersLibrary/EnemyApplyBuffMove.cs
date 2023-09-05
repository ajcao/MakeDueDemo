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
    string BuffName;
    int? Intensity;
    int? Duration;
    
    public EnemyApplyBuffMove (EnemyCharacter InputC, Character[] CArray, string BN, int? I, int? D)
    {
        TargetArray = CArray;
        Special = false;
        EC = InputC;
        BuffName = BN;
        this.Intensity = I;
        this.Duration = D;
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
        Buff B = BuffLibrary.GetBuffFromName(BuffName, C, EC, Intensity, Duration);
        BattleLogicHandler.OnBuffApply(B);
    }

    public override void AdditionalMoveDeletion()
    {
        return;
    }
    
    public override string MoveIndicatorText()
    {
        return "+" + this.Intensity;
    }
}

}
