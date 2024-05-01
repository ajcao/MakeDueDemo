using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using TMPro;
using AbilityUtil;
using TooltipUtil;
using TriggerEventUtil;

public class AbilityButtonScript : MonoBehaviour, TooltipStringInterface
{    
    public AbilityButtonHandler AB;
    private Ability AssignedAbility;
    public TextMeshProUGUI CooldownText;
    public TextMeshProUGUI ValueText;
    
    public void Init(AbilityButtonHandler InputAB)
    {
        AB = InputAB;
    }
    
    public void DefineAbility(Ability A)
    {
        AssignedAbility = A;        
    }
    

    public void onButtonClick()
    {
        if (AB.GetCurrentAbility() != AssignedAbility)
        {
            AB.SetCurrentAbility(AssignedAbility);
        }
        else //User selects the same abiltiy, deselect ability
        {
            AB.SetCurrentAbility(null);
        }
        
    }

    //Update cooldown
    public void Update()
    {
        if (AssignedAbility == null)
        {
            return;
        }

        if ((AssignedAbility.getCooldown() <= 0))
        {
            CooldownText.text = "";
        }
        else
        {
            CooldownText.text = AssignedAbility.getCooldown() + "";
        }

        if ( (AssignedAbility.GetType() == typeof(AttackAbility)) )
        {
            ValueText.text = "" + (AssignedAbility.PC.getAttackStat() + AssignedAbility.PC.getDamageOutputModifier()); 
        }
        else if ((AssignedAbility.GetType() == typeof(DefendAbility)))
        {
            ValueText.text = "" + (AssignedAbility.PC.getDefenseStat() + AssignedAbility.PC.getDefenseOutputModifier());
        }
        else
        {
            ValueText.text = "";
        }
    }

    public string GetTooltipString()
    {
        return AssignedAbility.GetTooltipString();
    }
}
