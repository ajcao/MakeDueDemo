using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using AbilityUtil;
using TooltipUtil;

public class AbilityButtonScript : MonoBehaviour, TooltipStringInterface
{    
    private AbilityButtonHandler AB;
    private Ability AssignedAbility;
    
    
    public void Init(AbilityButtonHandler inputAB)
    {
        AB = inputAB;
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

    public string GetTooltipString()
    {
        return AssignedAbility.GetTooltipString();
    }
}
