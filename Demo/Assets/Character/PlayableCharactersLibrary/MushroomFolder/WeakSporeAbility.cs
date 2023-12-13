using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class WeakSporeAbility : Ability
{
    public WeakSporeAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 5;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/MushroomAbilities/WeakSpore") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        Buff B = new WeakBuff(E, this.getPlayableCharacter(), null, 3);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Hallucinogenic Spore";
        string s1 = "Apply weak to enemy for 3 turns. Enemy deals 50% less damage";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}