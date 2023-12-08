using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class MagicMissileCannedItemBuff : Buff
{
    public MagicMissileCannedItemBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerActivateResolveEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/MagicMissileCanned");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Whenever you proc resolve, deal 30 damage to random enemy";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPlayerActivateResolveTrigger TE = (onPlayerActivateResolveTrigger) E;
        if (TE.CastingPlayer == this.BuffTarget)
        {
            List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
            int r = Random.Range(0,CurrentEncounter.Count);
            EnemyCharacter Enem = CurrentEncounter[r].GetComponent<EnemyCharacter>();
            BattleLogicHandler.BuffDamage(Enem, 30);
        }
    }
}

}
