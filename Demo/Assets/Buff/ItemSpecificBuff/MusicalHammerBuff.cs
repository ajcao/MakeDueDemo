using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriggerEventUtil;
using CharacterUtil;
using TooltipUtil;

namespace BuffUtil
{
    
public class MusicalHammerBuff : Buff
{
    public MusicalHammerBuff(Character CTarget, Character CBuffer, int Inten, int? Dur) 
    {
        this.Trigger = TriggerEventEnum.onDealArmorDamagePostEnum;
        this.TriggerSecondary = TriggerEventEnum.noTriggerEnum;
        this.BuffTarget = CTarget;
        this.OriginalBuffer = CBuffer;
        this.Intensity = Inten;
        this.Duration = Dur;
        this.Visible = true;
        this.Stackable = true;
        
        BuffIcon = Resources.Load<Sprite>("ItemImages/MusicalHammer");
    }

    
    public override void onApplication()
    {
    }
    
    public override void onExpire()
    {
    }
    
    public override string GetTooltipString()
    {
        string s1 = "If your attack breaks an enemy armor, deal " + this.Intensity.Value + " poise damage";
        return s1;
    }
    
    public override void onTriggerEffect(TriggerEvent E, ref int v)
    {
        onDealArmorDamagePostTrigger T = (onDealArmorDamagePostTrigger) E;
        if (T.AttackingChar == (Character) BuffTarget)
        {
            //Ensure the armor damage amount is above 0
            //And the enemy has no armor
            //This can be used to conclude the player broke the enemy armor
            if (T.ArmorAmount > 0 && T.ReceivingChar.getCurrentArmor() == 0)
            {
                EnemyCharacter Enem = (EnemyCharacter) T.ReceivingChar;
                BattleLogicHandler.LowerPoise(Enem, this.Intensity.Value);
            }
            
        }
        
    }
}

}
