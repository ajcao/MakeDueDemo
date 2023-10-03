using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using UnityEngine.UI;
using TooltipUtil;


namespace AbilityUtil
{
    
public enum TargetingTypeEnum
{
	EnemyTarget,
    PlayerTarget,
    NoTarget,
}
    
    
public abstract class Ability
{
    //Ability Owner
    public PlayableCharacter PC;
    
    protected TargetingTypeEnum targetingType;
    protected int maxCooldown;
    protected int currentCooldown;
    protected Sprite AbilityIcon;
    
    
    public void AssignCharacter(PlayableCharacter inputC)
    {
        this.PC = inputC;
    }
    
    public PlayableCharacter getPlayableCharacter()
    {
        return PC;
    }
    
    
    public TargetingTypeEnum getTargetingType()
    {
        return targetingType;
    }
    

    public int getMaxCooldown()
    {
        return maxCooldown;
    }
    
    public void setMaxCooldown(int c)
    {
        maxCooldown = c;
    }
    
    public void reduceCooldown()
    {
        if (currentCooldown > 0)
        {
            currentCooldown-=1;
        }
    }
    
    public Sprite getIcon()
    {
        return AbilityIcon;
    }
    
    public abstract void onCast(Character C);
    
    public abstract string GetTooltipString();
    
    public abstract void postCast(Character C);
    
    public virtual void postCastWrapper(Character C)
    {
        currentCooldown = maxCooldown;
        PC.setHasCasted(true);
        BattleLogicHandler.PostAbilityCast(this.PC, C);
        this.postCast(C);
    }
    
    public virtual bool canCast()
    {
        return (currentCooldown == 0);
    }
    
}

}
