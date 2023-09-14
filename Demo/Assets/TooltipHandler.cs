using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TooltipUtil
{
    
public class TooltipHandler : MonoBehaviour
{
    public Image TooltipBox;
    public TextMeshProUGUI TooltipText;
    
    private static string DisplayTooltipString = "";
    
    public static void GetToolTipString(string s)
    {
        DisplayTooltipString = s;
    }

    // Update is called once per frame
    void Update()
    {
        TooltipText.text = DisplayTooltipString;
        
        if (DisplayTooltipString == "")
        {
            TooltipBox.gameObject.transform.position = new Vector3(0, -300, 0);
        }
        else //tooltip exist
        {
            if (Input.mousePosition.x < (Screen.width / 2))
            {
                TooltipBox.gameObject.transform.position = Input.mousePosition + new Vector3(200,0,0);
            }
            else
            {
                TooltipBox.gameObject.transform.position = Input.mousePosition - new Vector3(200,0,0);
            }
        }
    }
}

}
