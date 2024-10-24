using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class MysteriousGooBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 120;
        this.MaxHealth = 140;
        this.CurrentArmor = 0;
        this.ArmorRetain = 20;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.AttackStat = 10;
        this.DefenseStat = 20;
        this.Resolve = 0;
        this.MaxResolve = 100;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);
        
        CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/MysteriousGooIcon");
        CharacterName = "Mysterious Goo";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new ApplyVulnurableAbilty(this));
        this.AbilityPool.Add(new AcidAttackAbillity(this));        
        this.AbilityPool.Add(new ShareArmorAbility(this));
        this.AbilityPool.Add(new ArmorAbsorbAbility(this));

        
    }
    
    public override string GetLoreData()
    {
        string hp = String.Format("Max HP: {0}", this.MaxHealth);
        string resolve = String.Format("Max Resolve: {0}", this.MaxResolve);
        string attack = String.Format("Basic Attack: {0}",  this.AttackStat);
        string defense = String.Format("Basic Defend: {0}", this.DefenseStat);
        
        string lore = "Maghelm, the second largest city in Landeus, contains the very first industrial magic factory. Everyday, vast amounts of both basic and rare ingredients are shipped to Maghelm wither by cart or portal. These raw ingredients are manufactured into multiple goods such as pre-casted single-use spells or powerful enchanted items. However, the intensity and quantity of all the magic also produces a lot of dangerous waste products. The goo was dumped in the sewers where it eventually flowed into the ocean. However some of the toxic goo festered in the sewers, eventually gaining something resembling self-preservation.";
        
        return String.Format("{0}\n{1}\n{2}\n{3}\n\n\n{4}", hp, resolve, attack, defense, lore);
        
    }

}

