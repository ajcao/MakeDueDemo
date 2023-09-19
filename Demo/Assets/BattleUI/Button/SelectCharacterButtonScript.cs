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
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        AB.SetCurrentCharacter(C);
    }
    
    public PlayableCharacter GetAssignedCharacter()
    {
        return C;
    }
    
    public void Update()
    {
        if (C.isAlive())
        {
            this.gameObject.transform.position = Camera.main.WorldToScreenPoint(C.gameObject.transform.position + new Vector3(0, 2, 0));
        }
        else
        {
            this.gameObject.transform.position = new Vector3(0, -300, 0);
        }
    }
}
