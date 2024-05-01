using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class ApaxeItemBuff : Buff
{
    public ApaxeItemBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerBasicAttackEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/Apaxe");
    }

    
    public override void onApplication()
    {
        int Inten = this.Intensity.Value;
        BuffTarget.setDamageOutputModifier(BuffTarget.getDamageOutputModifier() + Inten);
    }
    
    public override void onExpire()
    {
        int Inten = this.Intensity.Value;
        BuffTarget.setDamageOutputModifier(BuffTarget.getDamageOutputModifier() - Inten);
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Increase damage by " + this.Intensity.Value + ".";
        string s2 = "Everytime you basic attack, inflict " + this.Intensity + " damange on two random enemies";
        return s1 + "\n" + s2;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPlayerBasicAttackTrigger TE = (onPlayerBasicAttackTrigger) E;
        if (TE.AttackingPlayer == this.BuffTarget)
        {
            
            if (EnemyEncounter.getEncounterSize() > 0)
            {
                List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
                int r = Random.Range(0,CurrentEncounter.Count);
                EnemyCharacter Enem = CurrentEncounter[r].GetComponent<EnemyCharacter>();
                BattleLogicHandler.BuffDamage(Enem, 20);
            }
            
            if (EnemyEncounter.getEncounterSize() > 0)
            {
                List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
                int r = Random.Range(0,CurrentEncounter.Count);
                EnemyCharacter Enem = CurrentEncounter[r].GetComponent<EnemyCharacter>();
                BattleLogicHandler.BuffDamage(Enem, 20);
            }
        }
    }
}

}
