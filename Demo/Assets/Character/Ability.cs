using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using UnityEngine.UI;


namespace AbilityUtil
{
    
public enum TargetingTypeEnum
{
	EnemyTarget,
    PlayerTarget,
    NoTarget,
    Passive
}
    
    
public abstract class Ability
{
    //Ability Owner
    public PlayableCharacter PC;
    
    protected TargetingTypeEnum targetingType;
    protected int manaCost;
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
    
    public int getManaCost()
    {
        return manaCost;
    }
    
    public void setManaCost(int m)
    {
        manaCost = m;
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
    
    public void postCast()
    {
        currentCooldown = maxCooldown;
    }
    
    public bool canCast()
    {
        return true;
    }
    
}

}
