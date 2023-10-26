using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class AttackDefendAbility : Ability
{
    public AttackDefendAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 4;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/AttackDefendAbility") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.AttackDamage(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
        BattleLogicHandler.GainArmor(PC, PC, PC.getDefenseStat() + PC.getDefenseOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC,(EnemyCharacter) C);
        BattleLogicHandler.PlayerDefend(PC,(PlayableCharacter) PC);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Attack and Defend at the same time";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}