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
        AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericBuff");
    }

    public override void onCast(Character C)
    {
        Buff B = BuffLibrary.GetBuffFromName(BuffName, EC, C, Intensity, Duration);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string MoveIndicatorText()
    {
        return "+" + this.Intensity;
    }
    
    public override void MoveTargetIndicatorText(GameObject Canvas, Character[] Target)
    {
        EnemyTargetingLibrary.TargetNGenericIndicator(Canvas, Target);
    }
}

}
