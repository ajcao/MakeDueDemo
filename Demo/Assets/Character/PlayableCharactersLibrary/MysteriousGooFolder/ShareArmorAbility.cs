using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class ShareArmorAbility : Ability
{
    public ShareArmorAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 2;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/ArmorShareAbility") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        int currentArmor = this.PC.getCurrentArmor();
        
        BattleLogicHandler.GainArmor(this.PC, E, currentArmor);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Give " + this.PC.getCurrentArmor() + " armor";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return s1 + "\n" + s2;
    }
    
}


}