using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextTurnButtonScript : MonoBehaviour
{
    private AbilityButtonHandler AB;
    private string ButtonFunction;
        
    // Start is called before the first frame update
    public void Init(AbilityButtonHandler InputAB)
    {
        AB = InputAB;
        ButtonFunction = "EndPlayerTurn";
    }

    public void EditButtonFunction(string s)
    {
        ButtonFunction = s;

        if (ButtonFunction == "EndPlayerTurn")
        {
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("NextTurnButtonImage") as Sprite;
            this.gameObject.gameObject.GetComponent<Button>().interactable = true;
        }

        else if (ButtonFunction == "IsEnemyTurn")
        {
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("EnemyTurnButton") as Sprite;
            this.gameObject.gameObject.GetComponent<Button>().interactable = false;
        }

        else if (ButtonFunction == "ReturnToTitleScene")
        {
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ResetBattleButton") as Sprite;
            this.gameObject.gameObject.GetComponent<Button>().interactable = true;
        }

        else if (ButtonFunction == "GoToNextScene")
        {
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("NextBattleButton") as Sprite;
            this.gameObject.gameObject.GetComponent<Button>().interactable = true;
        }
    }
    
    public void onButtonClick()
    {
        if (ButtonFunction == "EndPlayerTurn")
        {
            AB.ResetCasting();
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            //Destroy EnemyData
            GameObject EE_Data = GameObject.Find("EnemyEncounterDataGameObject");
            Destroy(EE_Data);

            SceneCoordinator.nextSceneInBattle();
        }
    }
}
