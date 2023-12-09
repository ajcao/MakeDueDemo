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
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GenericAbilityAttackTwice") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.AttackDamage(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
        BattleLogicHandler.AttackDamage(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) C);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Attack twice for " + (PC.getAttackStat() + PC.getDamageOutputModifier()) + " damage.";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}