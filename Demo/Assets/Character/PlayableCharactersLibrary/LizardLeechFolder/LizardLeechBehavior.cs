using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class LizardLeechBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 60;
        this.MaxHealth = 80;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.AttackStat = 20;
        this.DefenseStat = 10;
        this.Resolve = 0;
        this.MaxResolve = 60;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);
        
        CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/LizardleechIcon");
        CharacterName = "Lizard Leech";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new AttackHealAbility(this));
        this.AbilityPool.Add(new EnemyGivesHPAbilty(this));
        this.AbilityPool.Add(new DoubleAttackAbility(this));
        this.AbilityPool.Add(new HPForATKAbility(this));

        
    }
    
    public override string GetLoreData()
    {
        string hp = String.Format("Max HP: {0}", this.MaxHealth);
        string resolve = String.Format("Max Resolve: {0}", this.MaxResolve);
        string attack = String.Format("Basic Attack: {0}",  this.AttackStat);
        string defense = String.Format("Basic Defend: {0}", this.DefenseStat);
        
        string lore = "The LizardLeech is a parasitic animal that can be found throughout Landeus. The reptile prefers to live in semi-aquatic enviornments such as swamps, marshes, and bogs. However the LizardLeech have also spread into human settlements. The sewers of large cities make a perfect habitat for the animal. As such, these four-legged abominations are a common nuisance for many cityfolk, occasionally crawling out of pipes to steal blood at night. Currently the King's Council is searching for a solutin.";
        
        return String.Format("{0}\n{1}\n{2}\n{3}\n\n\n{4}", hp, resolve, attack, defense, lore);
        
    }

}

