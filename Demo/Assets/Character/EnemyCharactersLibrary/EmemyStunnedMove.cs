using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;
using TMPro;
using EnemyTargetingLibraryUtil;

namespace EnemyMoveUtil
{
    
public class EnemyStunnedMove : EnemyMove
{
    public EnemyStunnedMove()
    {
        Special = false;
        AbilityIcon = Resources.Load<Sprite>("AbilityImages/StunIcon");
        TargetArray = new Character[0];
        
    }
    
    //Do nothing
    public override void onCast(Character C)
    {
        return;
    }
    
    public override string MoveIndicatorText()
    {
        return "";
    }
    
    public override void AdditionalMoveDeletion()
    {
        return;
    }
    
}

}
