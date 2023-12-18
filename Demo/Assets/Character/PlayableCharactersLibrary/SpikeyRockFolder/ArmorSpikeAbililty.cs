using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;

namespace AbilityUtil
{

public class ArmorSpikeAbility : Ability
{
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
        BattleLogicHandler.GainArmor(PC, P, 30 + PC.getDefenseOutputModifier());
        
        Buff B = new SpikeBuff(P, this.getPlayableCharacter(), 30 + PC.getDefenseOutputModifier(), 3);
        BattleLogicHandler.OnBuffApply(B);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerDefend(PC,(PlayableCharacter) PC);
    }
    
    public override string GetTooltipString()
    {
        string name = "Spike Cover";
        string s1 = "Give an ally player " + (30 + PC.getDefenseOutputModifier()) + " armor and " + (30 + PC.getDefenseOutputModifier()) + " spikes for 3 turn";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}