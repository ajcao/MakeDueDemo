using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class DoubleAttackAbility : Ability
{
    public DoubleAttackAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.manaCost = 0;
        this.maxCooldown = 0;
        this.currentCooldown = 0;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericAbilityAttackTwice") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
    }
    
}


}