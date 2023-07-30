using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffUtil;
using TMPro;
using TooltipUtil;

public class BuffIconScript : MonoBehaviour, TooltipStringInterface
{
    Buff B;
    public TextMeshPro Intensity;
    public TextMeshPro Duration;
    
    public void Init(Buff BInput)
    {
        B = BInput;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = B.getIcon();
    }

    // Update is called once per frame
    void Update()
    {
        if (B.getIntensity().HasValue)
            Intensity.text = "" + B.getIntensity().Value;
        else
            Intensity.text = "";
        
        if (B.getDuration().HasValue)
            Duration.text = "" + B.getDuration().Value;
        else
            Duration.text = "";
    }
    
    public string GetTooltipString()
    {
        return B.GetTooltipString();
    }
}
