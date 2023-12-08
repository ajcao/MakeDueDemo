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
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
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
        string s1 = "At the end of every 10th turn, deal 200 damage to all enemies";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onPreTurnTrigger T = (onPreTurnTrigger) E;
        int roundNum = BattleSceneHandler.GetRound();
        this.Intensity = roundNum;
        //Ensures two conditions:
        //Ensure it is the player's turn
        //Turn number is multiple of 10
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
        }
    }
}

}
