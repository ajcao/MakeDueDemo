using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using UnityEngine.UI;
using TooltipUtil;

namespace ItemUtil
{

public abstract class GameItem
{
    protected Sprite ItemIcon;
    
    protected PlayableCharacter ItemOwner;
    
    public bool isGlobal;
    
    public string ItemName;
    
    public Sprite getItemImage()
    {
        return ItemIcon;
    }
    public void AssignCharacter(PlayableCharacter C)
    {
        ItemOwner = C;
    }
    
    public abstract void OnApply();
    
    public PlayableCharacter getPlayableCharacter()
    {
        return ItemOwner;
    }
}

}
