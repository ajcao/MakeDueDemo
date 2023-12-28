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
        
        if (SceneCoordinator.GetBattleStatus(BattleString))
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else
            this.gameObject.GetComponent<Button>().interactable = true;
    }

    public void onButtonClick()
    {
        SceneCoordinator.BattleBeaten(BattleString);
        
        GameObject BattleData = Instantiate(BattleDataPrefab, new Vector2(0.0f,0.0f), Quaternion.identity) as GameObject;
        DontDestroyOnLoad(BattleData);
        BattleData.name = "EnemyEncounterDataGameObject";
        
        SceneCoordinator.LoadBattleEncounter();
    }
}
