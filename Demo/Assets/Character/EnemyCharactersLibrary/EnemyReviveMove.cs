using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using BuffUtil;
using EnemyTargetingLibraryUtil;

namespace EnemyMoveUtil
{
    
public class EnemyReviveMove : EnemyMove
{
    private (int,int)[] RespawnPoolAndLocationSlots;
    
    //CArray is only used for visual targeting
    public EnemyReviveMove (EnemyCharacter InputC, Character[] CArray, (int,int)[] Slots)
    {
        TargetArray = CArray;
        EC = InputC;
        RespawnPoolAndLocationSlots = Slots;
        if (EC == (EnemyCharacter) TargetArray[0])
            AbilityIcon = Resources.Load<Sprite>("AbilityImages/EnemySummon");
        else
            AbilityIcon = Resources.Load<Sprite>("AbilityImages/EnemyRevive");
        CanCastOnDead = true;
    }

    //Actual targets are the RespawnPoolSlots and EncounterLocationSlots
    public override void onCast(Character C)
    {
        foreach ((int RespawnPoolInt,int EncounterLocationInt) I in RespawnPoolAndLocationSlots)
        {
            EnemyEncounter.ReplaceEncounterMember(I.RespawnPoolInt, I.EncounterLocationInt);
        }
    }

    public override void AdditionalMoveDeletion()
    {
        return;
    }
    
    public override string MoveIndicatorText()
    {
        return "Summon";
    }
    
    public override string getAnimation()
    {
        if (EC == (EnemyCharacter) TargetArray[0])
        {
            return "Shake";
        }
        else
            return "Jump";
    }
}

}
