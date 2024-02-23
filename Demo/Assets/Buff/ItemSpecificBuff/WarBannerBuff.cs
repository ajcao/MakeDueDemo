using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class WarBannerBuff : Buff
{
    
    public WarBannerBuff(Character CTarget, Character CBuffer, int? Inten, int Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerAttackEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/WarBanner");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Every attack during the first turn, apply 2 vulnurable to all enemies";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPlayerAttackTrigger TE = (onPlayerAttackTrigger) E;
        if ((TE.AttackingPlayer == this.BuffTarget))
        {
            List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
            foreach (GameObject G in CurrentEncounter)
            {
                EnemyCharacter Enem = G.GetComponent<EnemyCharacter>();
                Buff B = new VulnurableBuff((Character) Enem, BuffTarget, null, 2);
                BattleLogicHandler.OnBuffApply(B);
                
            }
        }
    }
}

}
