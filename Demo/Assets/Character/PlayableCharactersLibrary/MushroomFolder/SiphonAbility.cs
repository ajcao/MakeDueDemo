using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class SiphonAbility : Ability
{
    private int basedamage = 30;
    public SiphonAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/MushroomAbilities/SiphonAbility") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        EnemyCharacter E = (EnemyCharacter) C;
        
        BattleLogicHandler.AttackDamage(PC, E, basedamage + PC.getDamageOutputModifier());
        BattleLogicHandler.GainResolve(PC, basedamage + PC.getDamageOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Mycelium Siphon";
        string s1 = "Deal " + (basedamage + PC.getDamageOutputModifier()) + " damage. Gain " + (basedamage + PC.getDamageOutputModifier()) + " Resolve";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}