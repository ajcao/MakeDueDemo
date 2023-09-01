using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class CorrosiveGooAbility : Ability
{
    public CorrosiveGooAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/CorrosiveGoo") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.LowerArmor(E, 20);
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) E, PC.getAttackStat() + PC.getDamageOutputModifier());
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Teak 20 damage to armor only. Then deal " + (PC.getAttackStat() + PC.getDamageOutputModifier()) + " damage.";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}