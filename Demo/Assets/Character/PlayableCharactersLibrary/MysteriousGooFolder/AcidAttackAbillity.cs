using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class AcidAttackAbillity : Ability
{
    private int basedamage = 20;
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
        BattleLogicHandler.LowerArmor(E, basedamage);
        BattleLogicHandler.AttackDamage(PC, (EnemyCharacter) E, basedamage + PC.getDamageOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Acid Attack";
        string s1 = "Remove " + basedamage + " armor from enemy. Then deal " + (basedamage + PC.getDamageOutputModifier()) + " damage.";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}