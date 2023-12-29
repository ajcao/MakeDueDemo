using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{
  
  
public class StunHitAbility : Ability
{
    private int staminaAmount = 40;
    private int basedamage = 20;

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
        BattleLogicHandler.LowerStamina(E, staminaAmount);
        BattleLogicHandler.AttackDamage(PC, E, basedamage + PC.getDamageOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Shell Drop";
        string s1 = "Stun for " + staminaAmount + ", then attack for " + (basedamage + PC.getDamageOutputModifier()) + " damage";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
}


}