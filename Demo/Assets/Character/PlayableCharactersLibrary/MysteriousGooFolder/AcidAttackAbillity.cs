using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class AcidAttackAbillity : Ability
{
    public AcidAttackAbillity(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GooAbillities/AcidSpray") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        BattleLogicHandler.LowerArmor(E, 20);
        BattleLogicHandler.AttackDamage(PC, (EnemyCharacter) E, 20 + PC.getDamageOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Acid Attack";
        string s1 = "Remove 20 armor from enemy. Then deal " + (20 + PC.getDamageOutputModifier()) + " damage.";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}