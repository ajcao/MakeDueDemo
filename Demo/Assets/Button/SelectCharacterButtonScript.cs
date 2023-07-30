using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterUtil;

public class SelectCharacterButtonScript : MonoBehaviour
{
    private AbilityButtonHandler AB;
    private PlayableCharacter C;
    
    public void Init(PlayableCharacter inputC, AbilityButtonHandler inputAB)
    {
        AB = inputAB;
        C = inputC;
    }
    
    public void onButtonClick()
    {
        AB.SetCurrentCharacter(C);
    }
    
    public PlayableCharacter GetAssignedCharacter()
    {
        return C;
    }
}
