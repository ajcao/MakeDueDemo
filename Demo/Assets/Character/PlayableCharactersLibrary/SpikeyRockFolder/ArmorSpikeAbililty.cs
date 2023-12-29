using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class ArmorSpikeAbility : Ability
{
    private int armorAmount = 30;
    private int duration = 3;

    public ArmorSpikeAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 4;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/SpikeyRockAbillities/GiveSpike") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        PlayableCharacter P = (PlayableCharacter) C;
        BattleLogicHandler.GainArmor(PC, P, armorAmount + PC.getDefenseOutputModifier());
        
        Buff B = new SpikeBuff(P, this.getPlayableCharacter(), armorAmount, duration);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerDefend(PC,(PlayableCharacter) PC);
    }
    
    public override string GetTooltipString()
    {
        string name = "Spike Cover";
        string s1 = "Give an ally player " + (armorAmount + PC.getDefenseOutputModifier()) + " armor and " + (armorAmount) + " spikes for " + duration + " turn";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}