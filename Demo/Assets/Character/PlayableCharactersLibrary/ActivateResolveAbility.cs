using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using UnityEngine.UI;
using BuffUtil;


namespace AbilityUtil
{
    
public class ActivateResolveAbility : Ability
{
    public ActivateResolveAbility(PlayableCharacter inputC)
    {
        this.AssignCharacter(inputC);
        targetingType = TargetingTypeEnum.NoTarget;
        currentCooldown = 0;
        maxCooldown = 0;
        
        this.AbilityIcon = Resources.Load<Sprite>("AbilityImages/ActivateResolve");
    }
    
    public override bool canCast()
    {
        return (this.PC.getResolve() == this.PC.getMaxResolve());
    }
    
    public override void onCast(Character E)
    {
        this.PC.setResolve(0);
        if (!this.PC.getHasCasted())
        {
            ResolveBuff B = new ResolveBuff(this.PC, this.PC, 1, null);
            BattleLogicHandler.OnBuffApply(B);
        }
        else
        {
            this.PC.RefreshCasting();
        }
        
    }
    
    public override void postCastWrapper(Character C)
    {
        postCast(C);
    }
    
    public override void postCast(Character C)
    {
        BattleLogicHandler.ActiveResolve((PlayableCharacter) C);
    }
    
    public override string GetTooltipString()
    {
        return "Use all resolve to take an extra turn. This ability can be casted whenever it is the player's turn";
    }
    
}

}
