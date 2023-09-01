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
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
        BattleLogicHandler.PlayerDefend(PC, (PlayableCharacter) PC, PC.getDefenseStat());
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Attack and Defend at the same time";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}