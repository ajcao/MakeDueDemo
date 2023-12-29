using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class AttackHealAbility : Ability
{
    private int basedamage = 30;
    
    public AttackHealAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 3;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/LizardLeechAbilities/GenericAbilityLIfestealAttack") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        EnemyCharacter E = (EnemyCharacter) C;
        
        int enemyHPBeforeAttack = E.getCurrentHealth();
        BattleLogicHandler.AttackDamage(PC, (EnemyCharacter) E, basedamage + PC.getDamageOutputModifier());
        int enemyHPAfterAttack = E.getCurrentHealth();
        
        BattleLogicHandler.GainHealth(PC, Mathf.Max(0, (enemyHPBeforeAttack - enemyHPAfterAttack)) );
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Bite";
        string s1 = "Deal " + (basedamage + PC.getDamageOutputModifier()) + " damage. Heal for total damage done to enemy health";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}