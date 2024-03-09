using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Storage script that gives easy access/collection for a canvas element
//with multiple InventorySingleBox gameobjects

//For shop, the GameObject player is null
//For personal inventory, player should be set to PlayableCharacter gameobject
public class CanvasInventoryScript : MonoBehaviour
{
    
    public GameObject Player;
    
    public GameObject[] InventorySlots;
}
