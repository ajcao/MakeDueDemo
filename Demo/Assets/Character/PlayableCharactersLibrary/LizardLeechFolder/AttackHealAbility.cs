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
        this.maxCooldown = 4;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/LizardLeechAbilities/GenericAbilityLIfestealAttack") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        EnemyCharacter E = (EnemyCharacter) C;
        
        int enemyHPBeforeAttack = E.getCurrentHealth();
        BattleLogicHandler.AttackDamage(PC, (EnemyCharacter) E, 30 + PC.getDamageOutputModifier());
        int enemyHPAfterAttack = E.getCurrentHealth();
        
        BattleLogicHandler.GainHealth(PC, (enemyHPAfterAttack - enemyHPBeforeAttack) );
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Bite";
        string s1 = "Deal " + (30 + PC.getDamageOutputModifier()) + " damage. Heal for total damage done to enemy health";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}