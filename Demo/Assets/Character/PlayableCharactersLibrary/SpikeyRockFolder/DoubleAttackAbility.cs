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
        this.currentCooldown = 0;
        this.maxCooldown = 4;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericAbilityAttackTwice") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Deal " + (PC.getAttackStat() + PC.getDamageOutputModifier()) + " damage twice";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}