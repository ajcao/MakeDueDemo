using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class ParryShieldItemBuff : Buff
{
    public ParryShieldItemBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerDefendEnum;
        this.TriggerSecondary = TriggerEventEnum.onArmorWasGainedEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/ParryShield");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Whenever you gain armor or defend another, \n";
        string s2 = "Deal " + this.Intensity + " damange to a random enemies";
        return s1 + "\n" + s2;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        
        if (E.GetType() == typeof(onPlayerDefendTrigger))
        {
            onPlayerDefendTrigger TE = (onPlayerDefendTrigger) E;
            
            if (TE.CastingPlayer == this.BuffTarget && TE.ReceivingPlayer != this.BuffTarget)
            {
                List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
                if (CurrentEncounter.Count > 0)
                {
                    int r = Random.Range(0,CurrentEncounter.Count);
                    EnemyCharacter Enem = CurrentEncounter[r].GetComponent<EnemyCharacter>();
                    BattleLogicHandler.BuffDamage(Enem, this.Intensity.Value);
                }
            }
        }
        
        else //Did character gain armor
        {
            onArmorWasGainedTrigger TE = (onArmorWasGainedTrigger) E;
            
            if (TE.ReceivingChar == this.BuffTarget)
            {
                List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
                if (CurrentEncounter.Count > 0)
                {
                    int r = Random.Range(0,CurrentEncounter.Count);
                    EnemyCharacter Enem = CurrentEncounter[r].GetComponent<EnemyCharacter>();
                    BattleLogicHandler.BuffDamage(Enem, this.Intensity.Value);
                }
            }
            
        }
        
    }
}

}
