using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (BattleSceneHandler.GetRound() > 0)
        {
            AB.ResetCasting();
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else
            SceneManager.LoadScene("BattleSelectionScene", LoadSceneMode.Single);
    }
}
