using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using BuffUtil;

public class SpikeyRockBehavior : PlayableCharacter
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
        this.AttackStat = 30;
        this.DefenseStat = 30;
        this.Resolve = 0;
        this.MaxResolve = 100;
        this.ResolveRegeneration = (int) Mathf.Ceil((float) this.MaxResolve / 2.0f);

        this.CharacterIcon = Resources.Load<Sprite>("PlayableCharacterImages/SpikeyRockIcon");
        CharacterName = "Spikey Rock";
        
        this.AbilityPool = new List<Ability>();
        this.AbilityPool.Add(new ActivateResolveAbility(this));
        this.AbilityPool.Add(new AttackAbility(this));
        this.AbilityPool.Add(new DefendAbility(this));
        this.AbilityPool.Add(new AttackDefendAbility(this));
        this.AbilityPool.Add(new GiveSelfSpikeAbility(this));        
        this.AbilityPool.Add(new ArmorSpikeAbility(this));
        this.AbilityPool.Add(new ArmorRetainAbility(this));


    }
    
    public override string GetLoreData()
    {
        string hp = String.Format("Max HP: {0}", this.MaxHealth);
        string resolve = String.Format("Max Resolve: {0}", this.MaxResolve);
        string attack = String.Format("Basic Attack: {0}",  this.AttackStat);
        string defense = String.Format("Basic Defend: {0}", this.DefenseStat);
        
        string lore = "To the northeast of Landeus lies Kanadar Valley/ The valley is unique for its interesting geographical features. Through means still unknown to geologists, the valley holds rocks with extreme spikes. These rocks appear as sparse groups dotted throughout various locations among the cliffs. The rocks pose an extreme hazard should one become unlodged and fall on a passerby. Powder from these rocks can enchance the hardness of armor when added during the smithing phase. However no industrious method of farming the powder has been created yet.";
        
        return String.Format("{0}\n{1}\n{2}\n{3}\n\n\n{4}", hp, resolve, attack, defense, lore);
        
    }

}
