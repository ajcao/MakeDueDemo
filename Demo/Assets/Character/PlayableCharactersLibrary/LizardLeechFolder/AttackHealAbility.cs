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
        this.manaCost = 0;
        this.maxCooldown = 0;
        this.currentCooldown = 0;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericAbilityLIfestealAttack") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
        BattleLogicHandler.Restore(PC, PC.getAttackStat() + PC.getDamageOutputModifier());
    }
    
}


}