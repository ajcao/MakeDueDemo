using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Coordinates scene loading for monster encounters
//Is the interface between BattleScelectionScene and the button

public static class SceneCoordinator
{
    public static bool PlantFightAttempted;
    public static bool RatFightAttempted;
    public static bool BeanFightAttempted;
    public static bool MonsterFightAttempted;
    public static bool FinalBossFightAttempted;

    public static bool ShopAttempted;
    
    public static void ResetBattleStatus()
    {
        PlantFightAttempted = false;
        RatFightAttempted = false;
        BeanFightAttempted = false;
        MonsterFightAttempted = false;
        FinalBossFightAttempted = false;
        ShopAttempted = false;
    }
    
    public static bool GetBattleStatus(string s)
    {
        switch (s)
        {
            case ("Plant"):
                return PlantFightAttempted;
            case ("Rat"):
                return RatFightAttempted;
            case ("RockBean"):
                return BeanFightAttempted;
            case ("Monster"):
                return MonsterFightAttempted;
            case ("FinalBoss"):
                return FinalBossFightAttempted;
            case ("Shop"):
                return ShopAttempted;
            default:
                return false;
        }
    }
    
    public static void BattleAttempted(string s)
    {
        switch (s)
        {
            case ("Plant"):
                PlantFightAttempted = true;
                break;
            case ("Rat"):
                RatFightAttempted = true;
                break;
            case ("RockBean"):
                BeanFightAttempted = true;
                break;
            case ("Monster"):
                MonsterFightAttempted = true;
                break;
            case ("FinalBoss"):
                FinalBossFightAttempted = true;
                break;
            case ("Shop"):
                ShopAttempted = true;
                break;
            default:
                break;
        }
    }

    public static bool NormalEncountersFinished()
    {
        return (PlantFightAttempted && RatFightAttempted && BeanFightAttempted && MonsterFightAttempted);
    }

    public static bool AllNormalEncounterAlive()
    {
        return (!PlantFightAttempted && !RatFightAttempted && !BeanFightAttempted && !MonsterFightAttempted);
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
    public static void LoadSecondShop()
    {
        SceneManager.LoadScene("SecondItemSelectionScene", LoadSceneMode.Single);
    }
    public static void nextSceneInBattle()
    {
        //Very start of the game
        if (AllNormalEncounterAlive())
            SceneManager.LoadScene("BattleSelectionScene", LoadSceneMode.Single);

        //Whether final boss is beaten or not, return to title screen
        else if (FinalBossFightAttempted)
            SceneManager.LoadScene("TitleScreenScene", LoadSceneMode.Single);

        //Player is leaving the shop to fight the final boss
        else if (ShopAttempted && !FinalBossFightAttempted)
            SceneManager.LoadScene("BattleSelectionScene", LoadSceneMode.Single);

        //Enemy is dead, continue battle
        else if (EnemyEncounter.IsEncounterDead())
            SceneManager.LoadScene("BattleSelectionScene", LoadSceneMode.Single);

        //Player is dead, return to title screen
        else if (PlayerParty.IsPartyDead())
            SceneManager.LoadScene("TitleScreenScene", LoadSceneMode.Single);

        else
            SceneManager.LoadScene("TitleScreenScene", LoadSceneMode.Single);
    }
}
