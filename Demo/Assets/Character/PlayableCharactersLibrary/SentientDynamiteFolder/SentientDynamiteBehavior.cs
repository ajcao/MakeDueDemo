using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class SentientDynamiteBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 140;
        this.MaxHealth = 160;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.AttackStat = 20;
        this.DefenseStat = 10;
        this.Resolve = 0;
        this.MaxResolve = 100;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);
        
        this.CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/SentientDynamiteIcon");
        CharacterName = "Sentient Dynamite";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new SmallAoeAttack(this));
        this.AbilityPool.Add(new NuclearAttackAbility(this));
        this.AbilityPool.Add(new ShrapnelAttack(this));
        this.AbilityPool.Add(new SmokeAbility(this));
    }

    public override string GetLoreData()
    {
        string hp = String.Format("Max HP: {0}", this.MaxHealth);
        string resolve = String.Format("Max Resolve: {0}", this.MaxResolve);
        string attack = String.Format("Basic Attack: {0}",  this.AttackStat);
        string defense = String.Format("Basic Defend: {0}", this.DefenseStat);
        
        string lore = "The sentient dynamite was the first conjuration spell of soon-to-be great wizard. Unlike other students who created birds or cats, she decided to create a bomb, perhaps in honor of her favorite alchemy class and her favorite substance, gunpowder. Currently she is serves as one of the chief advisors of Landeus, specializing in military and military research. Her life went through leaps of oppurtunities and changes, which could be a reason why she forgot her first success.";
        
        return String.Format("{0}\n{1}\n{2}\n{3}\n\n\n{4}", hp, resolve, attack, defense, lore);
        
    }
    
}

