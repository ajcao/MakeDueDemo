using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using TMPro;
using EnemyTargetingLibraryUtil;
using BuffUtil;

namespace EnemyMoveUtil
{
    
public class EnemyAttackDefendMove : EnemyMove
{
    int damageAmount;
    int armorAmount;
    
    public EnemyAttackDefendMove(EnemyCharacter inputC, int d, int a, Character[] CArray)
    {
        EC = inputC;
        TargetArray = CArray;
        damageAmount = d;
        armorAmount = a;
        AbilityIcon = Resources.Load<Sprite>("AbilityImages/AttackDefendIcon");
        
    }
    
    public override void onCast(Character C)
    {
        BattleLogicHandler.AttackDamage(this.EC, C.GetComponent<PlayableCharacter>(), damageAmount + EC.getDamageOutputModifier());
        BattleLogicHandler.GainArmor(this.EC, this.EC, armorAmount + EC.getDefenseOutputModifier());
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
        return "" + dmgText + "/" + (armorAmount + EC.getDefenseOutputModifier());
    }
    
    public override string getAnimation()
    {
        return "EnemyAttack";
    }
}

}
