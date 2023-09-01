using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

public class DamageNumberHandler : MonoBehaviour
{
    public GameObject DamageNumberPrefab;

    public void CreateDamageNumber(Character C, int d)
    {
        GameObject G = Instantiate(DamageNumberPrefab, C.gameObject.transform.position, Quaternion.identity) as GameObject;
        G.GetComponent<DamageNumberScript>().Init(d);
    }
}
