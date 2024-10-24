using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class ORBBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 60;
        this.MaxHealth = 80;
        this.CurrentArmor = 0;
        this.ArmorRetain = 30;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.AttackStat = 10;
        this.DefenseStat = 30;
        this.Resolve = 0;
        this.MaxResolve = 120;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);
        
        this.CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/ORBIcon");
        CharacterName = "ORB";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new OrbResetCooldown(this));
        this.AbilityPool.Add(new OrbAttackArmor(this));
        this.AbilityPool.Add(new HealAbility(this));
        this.AbilityPool.Add(new GiveResolveAbility(this));
    }

    public override string GetLoreData()
    {
        string hp = String.Format("Max HP: {0}", this.MaxHealth);
        string resolve = String.Format("Max Resolve: {0}", this.MaxResolve);
        string attack = String.Format("Basic Attack: {0}",  this.AttackStat);
        string defense = String.Format("Basic Defend: {0}", this.DefenseStat);
        
        string lore = "ORB";
        
        return String.Format("{0}\n{1}\n{2}\n{3}\n\n\n{4}", hp, resolve, attack, defense, lore);
        
    }
    
}
