using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class ApplyVulnurableAbilty : Ability
{
    private int duration = 2;
    
    public ApplyVulnurableAbilty(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.EnemyTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 4;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GooAbillities/CorrosiveGoo") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        Buff B = new VulnurableBuff(E, this.getPlayableCharacter(), null, duration);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Corrosive Slime";
        string s1 = "Apply vulnurable to enemy for " + duration + " turns. Enemy take 50% more damage";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}