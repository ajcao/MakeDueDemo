using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class RolyPolyBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 80;
        this.MaxHealth = 100;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.AttackStat = 20;
        this.DefenseStat = 20;
        this.Resolve = 0;
        this.MaxResolve = 60;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);
        
        CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/RolyPolyIcon");
        CharacterName = "Rolypoly";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new DefensiveCurlAbility(this));
        this.AbilityPool.Add(new RollAbility(this));
        this.AbilityPool.Add(new BugTempDoubleDamageAbility(this));
        this.AbilityPool.Add(new StunHitAbility(this));

        
    }
    
    public override string GetLoreData()
    {
        string hp = String.Format("Max HP: {0}", this.MaxHealth);
        string resolve = String.Format("Max Resolve: {0}", this.MaxResolve);
        string attack = String.Format("Basic Attack: {0}",  this.AttackStat);
        string defense = String.Format("Basic Defend: {0}", this.DefenseStat);
        
        string lore = "The rolypolys are large 200 pounds insects with strong eusocial behaviors. The rolypolys can form large underground colony comprised of multiple tunnels and rooms. The rolypoly is usually non-aggressive to humans, but issues can arise if the colony inadvertently expands near human settlements. Disposing of these insects proves challenging due to their thick exoskeleton and swarming behavior";
        
        return String.Format("{0}\n{1}\n{2}\n{3}\n\n\n{4}", hp, resolve, attack, defense, lore);
        
    }

}

