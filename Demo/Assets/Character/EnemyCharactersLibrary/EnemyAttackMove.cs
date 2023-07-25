using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using TMPro;
using EnemyTargetingLibraryUtil;

namespace EnemyMoveUtil
{
    
public class EnemyAttackMove : EnemyMove
{
    int damageAmount;
    
    public EnemyAttackMove(EnemyCharacter inputC, int d, Character[] CArray)
    {
        EC = inputC;
        TargetArray = CArray;
        Special = false;
        damageAmount = d;
        AbilityIcon = Resources.Load<Sprite>("AbilityImages/AttackIcon");
        
    }
    
    public override void onCast()
    {
        foreach (Character C in TargetArray)
        {
            BattleLogicHandler.EnemyAttack(this.EC, C.GetComponent<PlayableCharacter>(), damageAmount + EC.getDamageOutputModifier());
        }
    }
    
    public override string MoveIndicatorText()
    {
        return "" + (damageAmount + EC.getDamageOutputModifier());
    }
    
    public override void MoveTargetIndicatorText(GameObject Canvas, Character[] Target)
    {
        EnemyTargetingLibrary.TargetNGenericIndicator(Canvas, Target);
    }
}

}
