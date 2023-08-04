using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterUtil;

public class DefenseResolveScript : MonoBehaviour
{
    PlayableCharacter AssignedC;
    GameObject[] CList = new GameObject[4];
    
    
    // Start is called before the first frame update
    public void Init(PlayableCharacter C)
    {
        AssignedC = C;
        int i = 0;
        foreach (Transform Child in transform)
        {
            CList[i] = Child.gameObject;
            Child.GetComponent<Image>().sprite = PlayerParty.getPartyMember(i).GetComponent<PlayableCharacter>().getCharacterIcon();
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
                CList[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                Color c = Color.white;
                c.a = 0.5f;
                CList[i].GetComponent<Image>().color = c;
            }
        }
    }
}
