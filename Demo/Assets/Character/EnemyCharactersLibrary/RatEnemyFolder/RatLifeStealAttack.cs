using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using TMPro;
using EnemyTargetingLibraryUtil;
using BuffUtil;

namespace EnemyMoveUtil
{
    
public class RatLifeStealAttack : EnemyMove
{
    int damageAmount;
    
    public RatLifeStealAttack(EnemyCharacter inputC, int d, Character[] CArray)
    {
        EC = inputC;
        TargetArray = CArray;
        damageAmount = d;
        AbilityIcon = Resources.Load<Sprite>("AbilityImages/RatLifeSteal");
        
    }
    
    public override void onCast(Character C)
    {
        BattleLogicHandler.AttackDamage(this.EC, C.GetComponent<PlayableCharacter>(), damageAmount + EC.getDamageOutputModifier());
        BattleLogicHandler.GainHealth(this.EC, damageAmount + EC.getDamageOutputModifier());
    }
    
    public override void AdditionalMoveDeletion()
    {
    }
    
    public override string MoveIndicatorText()
    {
        int dmgText = damageAmount + EC.getDamageOutputModifier();
        if (BuffHandler.CharacterHaveBuff((Character) EC, new WeakBuff(EC, EC, null, null), false))
        {
            dmgText = dmgText / 2;
        }
        return "" + dmgText + "hp";
    }
    
    public override string getAnimation()
    {
        return "EnemyAttack";
    }
}

}
