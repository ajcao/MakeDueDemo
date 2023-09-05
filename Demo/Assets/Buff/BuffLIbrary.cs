using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace BuffUtil
{
    
public static class BuffLibrary
{
    public static Buff GetBuffFromName(string name, Character CTarget, Character CBuffer, int? Inten, int? Dur)
    {
        if (name == "AttackUpBuff")
        {
            return new AttackUpBuff(CTarget, CBuffer, Inten.Value, Dur);
        }
        
        if (name == "GiveHPWhenAttackedDebuff")
        {
            return new GiveHPWhenAttackedDebuff(CTarget, CBuffer, Inten.Value, Dur);
        }
        
        if (name == "GainArmorBuff")
        {
            return new GainArmorBuff(CTarget, CBuffer, Inten.Value, Dur);
        }
        
        if (name == "VulnurableBuff")
        {
            return new VulnurableBuff(CTarget, CBuffer, null, Dur);
        }
        
        return new NullBuff(CTarget, CBuffer, Inten, Dur);
    }
}

}
