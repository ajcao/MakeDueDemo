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
        
    }
    
    //Do nothing
    public override void onCast()
    {
        return;
    }
    
    public override string MoveIndicatorText()
    {
        return "";
    }
    
    public override void MoveTargetIndicatorText(GameObject Canvas, Character[] Target)
    {
        return;
    }
    
}

}
