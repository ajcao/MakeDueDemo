using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextTurnButtonScript : MonoBehaviour
{
    private AbilityButtonHandler AB;
    private bool GoToNextSceneBool = false;
        
    // Start is called before the first frame update
    public void Init(AbilityButtonHandler InputAB)
    {
        AB = InputAB;
    }

    public void SetToNextScene()
    {
        this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("NextBattleButton") as Sprite;
        this.gameObject.gameObject.GetComponent<Button>().interactable = true;
        GoToNextSceneBool = true;
    }
    
    public void onButtonClick()
    {
        if (!GoToNextSceneBool)
        {
            AB.ResetCasting();
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else
            SceneManager.LoadScene("BattleSelectionScene", LoadSceneMode.Single);
    }
}
