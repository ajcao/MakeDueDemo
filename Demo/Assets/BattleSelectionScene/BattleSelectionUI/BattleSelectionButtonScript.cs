using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BattleSelectionButtonScript : MonoBehaviour
{
    public string BattleString;
    public GameObject BattleDataPrefab;
    public TextMeshProUGUI TextBox;
    
    // Start is called before the first frame update
    void Start()
    {
        TextBox.text = BattleString;

        //If there is a normal encounter left, the shop and boss are hidden
        //if ((BattleString == "Shop" || BattleString == "FinalBoss") && (!SceneCoordinator.NormalEncountersFinished()))
        if (false)
            this.gameObject.SetActive(false);

        if (SceneCoordinator.GetBattleStatus(BattleString))
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else
            this.gameObject.GetComponent<Button>().interactable = true;
    }

    public void onButtonClick()
    {
        //Mark Battle as beaten/attempted
        SceneCoordinator.BattleAttempted(BattleString);

        if (BattleString != "Shop")
        {
            GameObject BattleData = Instantiate(BattleDataPrefab, new Vector2(0.0f, 0.0f), Quaternion.identity) as GameObject;
            DontDestroyOnLoad(BattleData);
            BattleData.name = "EnemyEncounterDataGameObject";

            SceneCoordinator.LoadBattleEncounter();
        }
        else //String is a shop
        {
            SceneCoordinator.LoadSecondShop();
        }
    }
}
