using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class PalmTreeBehavior : PlayableCharacter
{
    //Initalize Stats
    public void Awake()
    {
        this.Alive = true;
        this.HasCasted = false;
        this.CurrentHealth = 100;
        this.MaxHealth = 120;
        this.CurrentArmor = 0;
        this.ArmorRetain = 20;
        this.DamageOutputModifier = 0;
        this.DefenseOutputModifier = 0;
        this.AttackStat = 30;
        this.DefenseStat = 20;
        this.Resolve = 0;
        this.MaxResolve = 40;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);
        
        CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/PalmTreeIcon");
        CharacterName = "Potted Palm Tree";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new CoconutMilkAbility(this));
        this.AbilityPool.Add(new CoconutStunAbility(this));
        this.AbilityPool.Add(new CoconutAoeStunAbillity(this));
        this.AbilityPool.Add(new CoconutArmorAbility(this));

        
    }

    public override string GetLoreData()
    {
        string hp = String.Format("Max HP: {0}", this.MaxHealth);
        string resolve = String.Format("Max Resolve: {0}", this.MaxResolve);
        string attack = String.Format("Basic Attack: {0}",  this.AttackStat);
        string defense = String.Format("Basic Defend: {0}", this.DefenseStat);
        
        string lore = "The southern coast of Landeus contains trees famous for their towering heights, averaging about 65 meters. Another noticable feature is the seeds--Large brown husks with a hard exterior and nutritious fleshy interior. The seeds of the tree require a lot of nutrients and space to grow, making it tough to grow this tree outside its native enviornment. As such, it is interesting how one managed to grow within a comparatively small pot.";
        
        return String.Format("{0}\n{1}\n{2}\n{3}\n\n\n{4}", hp, resolve, attack, defense, lore);
        
    }
    
}

