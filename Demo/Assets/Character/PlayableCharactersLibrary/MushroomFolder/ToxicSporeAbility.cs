using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class ToxicSporeAbility : Ability
{
    public ToxicSporeAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 3;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/ToxicSpore") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        Buff B = new ToxicSporeBuff(E, this.getPlayableCharacter(), 30, 1);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Debuff enemy. When enemy is attacked, damage for 30 hp";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}