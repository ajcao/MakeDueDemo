using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using UnityEngine.SceneManagement;

public class StartBattleButtonScript : MonoBehaviour
{    
    public void OnButtonClick()
    {
        Debug.Log("Button Click");
        foreach (GameObject PC in PlayerParty.getParty())
        {
            DontDestroyOnLoad(PC);
        }
        
        //Assign Items to characters
        
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
    }
}
