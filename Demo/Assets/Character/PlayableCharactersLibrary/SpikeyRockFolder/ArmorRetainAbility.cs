using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class ArmorRetainAbility : Ability
{
    private int armorAmount = 40;
    public ArmorRetainAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 5;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/SpikeyRockAbillities/RockPowder") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        PlayableCharacter P = (PlayableCharacter) C;
        BattleLogicHandler.GainArmor(PC, P, armorAmount + PC.getDefenseOutputModifier());
        
        Buff B = new RetainBuff(P, this.getPlayableCharacter(), (armorAmount + PC.getDefenseOutputModifier()), null);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerDefend(PC,(PlayableCharacter) PC);
    }
    
    public override string GetTooltipString()
    {
        string name = "Rock Powder";
        string s1 = "Give an ally player " + (armorAmount + PC.getDefenseOutputModifier()) + " armor and " + (armorAmount + PC.getDefenseOutputModifier()) + " armor retain";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}