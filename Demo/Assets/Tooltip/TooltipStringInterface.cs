using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooltipUtil
{
    
//Should be put on the primary script of the prefab gameobject.
//Tooltip script is on the prefab, and gets the information from the prefab script, which has this interface
public interface TooltipStringInterface
{
    public string GetTooltipString();
}

}
