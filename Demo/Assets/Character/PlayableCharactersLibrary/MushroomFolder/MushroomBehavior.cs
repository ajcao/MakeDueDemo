using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class MushroomBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 100;
        this.MaxHealth = 120;
        this.CurrentArmor = 0;
        this.ArmorRetain = 0;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.AttackStat = 20;
        this.DefenseStat = 20;
        this.Resolve = 0;
        this.MaxResolve = 80;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);
        
        CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/MushroomIcon");
        CharacterName = "Mushroom";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new MushroomHealAbility(this));
        this.AbilityPool.Add(new ToxicSporeAbility(this));
        this.AbilityPool.Add(new WeakSporeAbility(this));
        this.AbilityPool.Add(new SiphonAbility(this));

        
    }
    
    public override string GetLoreData()
    {
        string hp = String.Format("Max HP: {0}", this.MaxHealth);
        string resolve = String.Format("Max Resolve: {0}", this.MaxResolve);
        string attack = String.Format("Basic Attack: {0}",  this.AttackStat);
        string defense = String.Format("Basic Defend: {0}", this.DefenseStat);
        
        string lore = "This is a standard mushroom. There is nothing particularly interesting about this one. The mushroom uses dense mycelium roots to gather nutrients from the soil. The spores of the mushroom are slightly toxic, causing a hallugenic effect when entering the bloodstream by inhalation or injection. Finally, the pileus of the mushroom is edible and is used as a alternative health option in multiple countries";
        
        return String.Format("{0}\n{1}\n{2}\n{3}\n\n\n{4}", hp, resolve, attack, defense, lore);
        
    }

}

