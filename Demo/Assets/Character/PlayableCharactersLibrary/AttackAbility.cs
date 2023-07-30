using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class AttackAbility : Ability
{
    public AttackAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.EnemyTarget;
        currentCooldown = 0;
        maxCooldown = 0;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/AttackIcon");
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
    }
    
    public override string GetTooltipString()
    {
        return "Deal " + (PC.getAttackStat() + PC.getDamageOutputModifier()) + " damage";
    }
}


}