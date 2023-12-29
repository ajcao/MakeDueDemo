using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class CoconutArmorAbility : Ability
{

    private int basearmor = 30;
    private int duration = 3;
    public CoconutArmorAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 3;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/PalmTreeAbilities/CoconutArmor") as Sprite;
    }
    
    public override void onCast(Character E)
    {
        Buff B = new GainArmorBuff(E, this.PC, (basearmor + this.PC.getDefenseOutputModifier()), 3);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Coconut Armor";
        string s1 = "Give an ally player a buff that restores " + (basearmor + PC.getDefenseOutputModifier()) + " armor per turn. Buff lasts " + duration + " turns";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}