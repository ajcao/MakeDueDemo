using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using UnityEngine.SceneManagement;
using ItemUtil;
using TMPro;

public class TitleScreenTransitionToNextSceneScript : MonoBehaviour
{
    public string SceneType;
    public TextMeshProUGUI TextBox;

    void Start()
    {
        TextBox.text = SceneType;
    }

    public void OnButtonClick()
    {
        switch (SceneType)
        {
            case ("StartGame"):
                //Reset BattleScene
                SceneCoordinator.ResetBattleStatus();
                SceneManager.LoadScene("ItemSelectionScene", LoadSceneMode.Single);
                break;
            case ("HowToPlay"):
                SceneManager.LoadScene("HowToPlayScene", LoadSceneMode.Single);
                break;
            case ("Story"):
                break;
            default:
                break;
        }
    }
}
