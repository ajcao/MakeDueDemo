using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class ArmorMilkAbility : Ability
{
    public ArmorMilkAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 3;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/ArmorMilk") as Sprite;
    }
    
    public override void onCast(Character P)
    {
        Buff B = new GainArmorBuff(P, this.getPlayableCharacter(), 20, 4);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC,C);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Gain a buff that gives 20 armor for 4 turns at the start of the turn";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}