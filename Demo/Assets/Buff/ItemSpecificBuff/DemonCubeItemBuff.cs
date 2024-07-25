using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class DemonCubeItemBuff : Buff
{
    public DemonCubeItemBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onPreTurnEnum;
        this.TriggerSecondary = TriggerEventEnum.onPostTurnEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/DemonsCube");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "Every 10 turns of full resolve, deal 200 damage to all enemies";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        //Uses preturn to increment counter by 1 during start of player turn
        if (E.GetType() == typeof(onPreTurnTrigger))
        {
            onPreTurnTrigger T = (onPreTurnTrigger) E;
            if (this.BuffTarget.GetType().IsSubclassOf(T.CharacterType))
            {
                PlayableCharacter PC = this.BuffTarget as PlayableCharacter;
                if (PC.getResolve() == PC.getMaxResolve())
                {
                    this.Intensity++;
                }
            }
        }

        //Explosion occurs at end of player turn
        if (E.GetType() == typeof(onPostTurnTrigger))
        {
            onPostTurnTrigger T = (onPostTurnTrigger) E;
            
            if (this.BuffTarget.GetType().IsSubclassOf(T.CharacterType) && (this.Intensity % 10) == 0)
            {
                List<GameObject> CurrentEncounter = EnemyEncounter.GetLivingEncounterMembers();
                foreach (GameObject G in CurrentEncounter)
                {
                    EnemyCharacter Enem = G.GetComponent<EnemyCharacter>();
                    if (Enem.isAlive() && this.BuffTarget.isAlive())
                    {
                        BattleLogicHandler.BuffDamage(Enem, 200);
                    }
                    
                }
                this.Intensity = 0;
            }
        }
    }
}

}
