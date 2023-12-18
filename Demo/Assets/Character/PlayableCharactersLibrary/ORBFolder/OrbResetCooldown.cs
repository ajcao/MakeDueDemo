using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace AbilityUtil
{

public class OrbResetCooldown : Ability
{
    public OrbResetCooldown(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        this.targetingType = TargetingTypeEnum.PlayerTarget;
        this.currentCooldown = 0;
        this.maxCooldown = 10;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/OrbAbilities/OrbResetCooldown") as Sprite;
    }
    
    public override void onCast(Character C)
    {
        PlayableCharacter P = (PlayableCharacter) C;
        P.ResetAllCooldown();
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.PlayerSkill(PC, C);
    }
    
    public override string GetTooltipString()
    {
        string name = "Harmonic Wave";
        string s1 = "Reset a character's cooldown (this ability does not reset if self-casted)";
        string s2 = "Cooldown: " + currentCooldown + "/" + maxCooldown;
        return name + "\n" + s1 + "\n" + s2;
    }
    
}


}