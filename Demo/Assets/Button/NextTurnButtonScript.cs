using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTurnButtonScript : MonoBehaviour
{
    private AbilityButtonHandler AB;
        
    // Start is called before the first frame update
    public void Init(AbilityButtonHandler InputAB)
    {
        AB = InputAB;
    }

    public void onButtonClick()
    {
        AB.SetCastingMode(false);
        this.gameObject.GetComponent<Button>().interactable = false;
    }
}
