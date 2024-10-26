using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class MasterBuff : Buff
{
    public MasterBuff(Character CTarget, Character CBuffer, int? Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.noTriggerEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("AbilityImages/MasterBuff");
    }

    
    public override void onApplication()
    {
    }
    
    //If user is death damage all allies
    public override void onExpire()
    {
        if (!this.BuffTarget.isAlive())
        {        
            foreach (GameObject G in EnemyEncounter.GetLivingEncounterMembers())
            {
                EnemyCharacter EC = G.GetComponent<EnemyCharacter>();
                if (EC.isAlive())
                {
                    BattleLogicHandler.BuffDamage(EC, 1000);
                }
            }
            
        }
    }
    
    public override string GetTooltipString()
    {
        string s1 = "On user death allies takes huge damage";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
    }
}

}
