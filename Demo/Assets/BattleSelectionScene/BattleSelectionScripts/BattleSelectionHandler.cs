using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using UnityEngine.UI;
using ItemUtil;

public class BattleSelectionHandler : MonoBehaviour
{
    void Start()
    {
        PlayerParty.getPartyMember(0).transform.position = new Vector3(0, -500, 0);
        PlayerParty.getPartyMember(1).transform.position = new Vector3(0, -500, 0);
        PlayerParty.getPartyMember(2).transform.position = new Vector3(0, -500, 0);
        PlayerParty.getPartyMember(3).transform.position = new Vector3(0, -500, 0);
    }
}
