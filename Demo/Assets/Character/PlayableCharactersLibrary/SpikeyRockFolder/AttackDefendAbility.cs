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
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/SpikeyRockAbillities/AttackDefendAbility") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.AttackDamage(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
        BattleLogicHandler.GainArmor(PC, PC, PC.getDefenseStat() + PC.getDefenseOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerBasicAttack(PC, (EnemyCharacter) C);
        BattleLogicHandler.PlayerAttack(PC,(EnemyCharacter) C);
        BattleLogicHandler.PlayerBasicDefend(PC, PC);
        BattleLogicHandler.PlayerDefend(PC, PC);
    }
    
    public override string GetTooltipString()
    {
        string name = "Rock Roll";
        string s1 = "Perform a basic Attack (" + (PC.getAttackStat() + PC.getDamageOutputModifier()) + ") and a basic Defend (" + (PC.getDefenseStat() + PC.getDefenseOutputModifier()) + ")";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}