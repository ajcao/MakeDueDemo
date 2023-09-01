using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class CoconutStunAbility : Ability
{
    public CoconutStunAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 3;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/CoconutStun") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E, 10 + PC.getDamageOutputModifier());
        BattleLogicHandler.LowerStamina((EnemyCharacter) E, 50);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Deal " + (10 + PC.getDamageOutputModifier()) + " damage, followed by 50 stamina damage";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}