using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static void LoadBattleEncounter(string s)
    {
        switch (s)
        {
            case ("Plant"):
                s = "GenericPlantBattleScene";
                break;
            case ("Monster"):
                s = "GenericMonsterBattleScene";
                break;
            case ("RockBean"):
                s = "GenericEnemyBattleScene";
                break;
            case ("Rat"):
                s = "RatEnemyEncounter";
                break;
            default:
                s = "GenericEnemyBattleScene";
                break;
        }
        SceneManager.LoadScene(s, LoadSceneMode.Single);
    }
}
