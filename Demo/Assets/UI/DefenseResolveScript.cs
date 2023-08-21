using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterUtil;
using UnityEngine.EventSystems;

public class DefenseResolveScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    PlayableCharacter AssignedC;
    public SpriteRenderer[] CharacterList;
    
    private Coroutine HighlightCharactersRoutine;
    
    
    // Start is called before the first frame update
    public void Init(PlayableCharacter C)
    {
        AssignedC = C;
        int i = 0;
        foreach (SpriteRenderer SR in CharacterList)
        {
            SR.sprite = PlayerParty.getPartyMember(i).GetComponent<PlayableCharacter>().getCharacterIcon();
            i++;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        (GameObject C, bool b)[] PList = AssignedC.getProtectionList();
        for (int i = 0; i < PList.Length; i++)
        {
            if (PList[i].b)
            {
                CharacterList[i].color = Color.yellow;
            }
            else
            {
                Color c = Color.white;
                c.a = 0.5f;
                CharacterList[i].color = c;
            }
        }
    }
    
    //Used to highlight targets
    public void OnPointerEnter(PointerEventData eventData)
	{
		HighlightCharactersRoutine = StartCoroutine(HighlightCharacters());
	}
    
    IEnumerator HighlightCharacters()
    {
        while (true)
        {
            (GameObject C, bool b)[] PList = AssignedC.getProtectionList();
            foreach ((GameObject C, bool b) in PList)
            {
                if (b)
                {
                    C.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                else
                {
                    C.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            yield return null;
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
	{
		StopCoroutine(HighlightCharactersRoutine);
        
        (GameObject C, bool b)[] PList = AssignedC.getProtectionList();
        foreach ((GameObject C, bool b) in PList)
        {
            C.GetComponent<SpriteRenderer>().color = Color.white;
        }
	}
}
