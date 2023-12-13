using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class SiphonAbility : Ability
{
    public SiphonAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 3;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/MushroomAbilities/SiphonAbility") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        EnemyCharacter E = (EnemyCharacter) C;
        
        BattleLogicHandler.AttackDamage(PC, E, 30 + PC.getDamageOutputModifier());
        BattleLogicHandler.GainResolve(PC, 30 + PC.getDamageOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
        BattleLogicHandler.PlayerAttack(PC, (EnemyCharacter) C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Mycelium Siphon";
        string s1 = "Deal " + (30 + PC.getDamageOutputModifier()) + " damage. Absorb " + (30 + PC.getDamageOutputModifier()) + " Resolve";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}