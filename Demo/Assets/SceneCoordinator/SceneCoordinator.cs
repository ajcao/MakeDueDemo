using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Coordinates scene loading for monster encounters
//Is the interface between BattleScelectionScene and the button

public static class SceneCoordinator
{
    public static bool BeatPlant;
    public static bool BeatRat;
    public static bool BeatBean;
    public static bool BeatMonster;
    
    public static void ResetBattleStatus()
    {
        BeatPlant = false;
        BeatRat = false;
        BeatBean = false;
        BeatMonster = false;
    }
    
    public static bool GetBattleStatus(string s)
    {
        switch (s)
        {
            case ("Plant"):
                return BeatPlant;
            case ("Rat"):
                return BeatRat;
            case ("RockBean"):
                return BeatBean;
            case ("Monster"):
                return BeatMonster;
            default:
                return false;
        }
    }
    
    public static void BattleBeaten(string s)
    {
        switch (s)
        {
            case ("Plant"):
                BeatPlant = true;
                break;
            case ("Rat"):
                BeatRat = true;
                break;
            case ("RockBean"):
                BeatBean = true;
                break;
            case ("Monster"):
                BeatMonster = true;
                break;
            default:
                break;
        }
    }
    
    //The Select Battle Scene buttons contain the data for the encounter as a prefab
    //Scene Coordinator only marks the battle as beaten/not beaten and loads the scene
    public static void LoadBattleEncounter()
    {
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
    }
    
    public static void LoadCharacterDataScene()
    {
        SceneManager.LoadScene("CharacterDataScene", LoadSceneMode.Additive);
    }
}
