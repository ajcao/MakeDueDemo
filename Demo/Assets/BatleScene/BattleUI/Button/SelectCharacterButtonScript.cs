using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterUtil;
using UnityEngine.EventSystems;

public class SelectCharacterButtonScript : MonoBehaviour, IPointerEnterHandler
{
    private AbilityButtonHandler AB;
    private PlayableCharacter C;
    
    public void Init(PlayableCharacter inputC, AbilityButtonHandler inputAB)
    {
        AB = inputAB;
        C = inputC;
        
        this.gameObject.transform.position = Camera.main.WorldToScreenPoint(C.gameObject.transform.position + new Vector3(0, 1, 0));
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        AB.SetCurrentCharacter(C);
    }
    
    public PlayableCharacter GetAssignedCharacter()
    {
        return C;
    }
    
}
