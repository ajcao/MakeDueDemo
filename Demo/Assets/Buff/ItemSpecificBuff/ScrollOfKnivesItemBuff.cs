using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class ScrollOfKnivesItemBuff : Buff
{
    public ScrollOfKnivesItemBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPlayerSkillEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/ScrollOfKnives");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Everytime two times a skill is cast, deal 30 damage to everyone";
        return s1;
    }
    
    int counter = 0;
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPlayerSkillTrigger T = (onPlayerSkillTrigger) E;
        if (T.CastingPlayer == (PlayableCharacter) BuffTarget)
        {
            counter+=1;
            
            this.Intensity = (counter % 2);
            
            if (this.Intensity == 1)
            {
                return;
            }
            
            List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
            foreach (GameObject G in CurrentEncounter)
            {
                EnemyCharacter Enem = G.GetComponent<EnemyCharacter>();
                if (Enem.isAlive() && this.BuffTarget.isAlive())
                {
                    BattleLogicHandler.BuffDamage(Enem, 30);
                }
                
            }        
        }
        
    }
}

}
