using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class StunHitAbility : Ability
{
    public StunHitAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 4;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/RolyPolyAbilities/PolyDrop") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        EnemyCharacter E = (EnemyCharacter) C;
        BattleLogicHandler.LowerStamina(E, 40);
        BattleLogicHandler.AttackDamage(PC, E, PC.getAttackStat() + PC.getDamageOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Shell Drop";
        string s1 = "Stun for 40, then attack for " + (PC.getAttackStat() + PC.getDamageOutputModifier()) + " damage";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
}


}