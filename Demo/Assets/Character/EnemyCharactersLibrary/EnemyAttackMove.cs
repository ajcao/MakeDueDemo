using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using TMPro;
using EnemyTargetingLibraryUtil;
using BuffUtil;

namespace EnemyMoveUtil
{
    
public class EnemyAttackMove : EnemyMove
{
    int damageAmount;
    
    public EnemyAttackMove(EnemyCharacter inputC, int d, Character[] CArray)
    {
        EC = inputC;
        TargetArray = CArray;
        damageAmount = d;
        AbilityIcon = Resources.Load<Sprite>("AbilityImages/AttackIcon");
        
    }
    
    public override void onCast(Character C)
    {
        BattleLogicHandler.AttackDamage(this.EC, C.GetComponent<PlayableCharacter>(), damageAmount + EC.getDamageOutputModifier());
    }
    
    public override void AdditionalMoveDeletion()
    {
    }
    
    public override string MoveIndicatorText()
    {
        int dmgText = damageAmount + EC.getDamageOutputModifier();
        if (BuffHandler.CharacterHaveBuff((Character)EC, new WeakBuff(EC, EC, null, null), false))
        {
            dmgText = dmgText / 2;
        }
        return "" + dmgText;
    }
    
    public override string getAnimation()
    {
        return "EnemyAttack";
    }
}

}
