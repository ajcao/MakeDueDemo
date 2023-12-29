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
        this.maxCooldown = 5;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/GooAbillities/ArmorShareAbility") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        int currentArmor = this.PC.getCurrentArmor();
        
        BattleLogicHandler.GainArmor(this.PC, E, currentArmor + PC.getDefenseOutputModifier());
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerDefend(PC, (PlayableCharacter) C);
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Discarded Armor";
        string s1 = "Give an ally player armor equal to the slime's current armor (" + (this.PC.getCurrentArmor() + PC.getDefenseOutputModifier()) + ")";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}