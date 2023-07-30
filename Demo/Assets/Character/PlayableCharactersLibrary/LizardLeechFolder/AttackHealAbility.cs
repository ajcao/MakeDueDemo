using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class AttackHealAbility : Ability
{
    public AttackHealAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericAbilityLIfestealAttack") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
        BattleLogicHandler.Restore(PC, PC.getAttackStat() + PC.getDamageOutputModifier());
    }
    
    public override string GetTooltipString()
    {
        return "Deal " + (PC.getAttackStat() + PC.getDamageOutputModifier()) + " damage. Heal for damage amount";
    }
    
}


}