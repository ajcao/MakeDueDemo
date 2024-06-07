using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterUtil;

namespace BuffUtil
{

public static class BuffLibrary
{
    //LEGACY CURRENLTY NOT USED
    public static List<Buff> GetBuffFromName(string[] name, Character CTarget, Character CBuffer, int? Inten, int? Dur)
    {
        List<Buff> returnBuff = new List<Buff>();
        foreach (string n in name)
        {
            if (n == "AttackUpBuff")
            {
                returnBuff.Add(new AttackUpBuff(CTarget, CBuffer, Inten.Value, Dur));
            }

            if (n == "DefenseUpBuff")
            {
                returnBuff.Add(new DefenseUpBuff(CTarget, CBuffer, Inten.Value, Dur));
            }

            if (n == "GiveHPWhenAttackedDebuff")
            {
                returnBuff.Add(new GiveHPWhenAttackedDebuff(CTarget, CBuffer, Inten.Value, Dur));
            }

            if (n == "GainArmorBuff")
            {
                returnBuff.Add(new GainArmorBuff(CTarget, CBuffer, Inten.Value, Dur));
            }

            if (n == "GainHPBuff")
            {
                returnBuff.Add(new GainHPBuff(CTarget, CBuffer, Inten.Value, Dur));
            }

            if (n == "VulnurableBuff")
            {
                returnBuff.Add(new VulnurableBuff(CTarget, CBuffer, null, Dur));
            }

            if (n == "FrailBuff")
            {
                returnBuff.Add(new FrailBuff(CTarget, CBuffer, null, Dur));
            }

            if (n == "SpikeBuff")
            {
                returnBuff.Add(new SpikeBuff(CTarget, CBuffer, Inten.Value, Dur));
            }

            if (n == "RetainBuff")
            {
                returnBuff.Add(new RetainBuff(CTarget, CBuffer, Inten.Value, Dur));
            }
        }
        return returnBuff;
    }
}

}
