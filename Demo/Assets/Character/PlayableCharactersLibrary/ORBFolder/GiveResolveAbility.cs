using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class GiveResolveAbility : Ability
{
    private int resolveAmount = 50;
    public GiveResolveAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 6;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/OrbAbilities/OrbGainResolve") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        PlayableCharacter P = (PlayableCharacter) C;
        BattleLogicHandler.GainResolve(P, resolveAmount);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Negative Charge";
        string s1 = "Restore " + resolveAmount + " resolve to an ally player";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}