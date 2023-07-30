using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooltipUtil;
using UnityEngine.EventSystems;

public class TooltipScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    private Coroutine TooltipDelayCoroutine;
    
	public void OnPointerEnter(PointerEventData eventData)
	{
		TooltipDelayCoroutine = StartCoroutine(TooltipDelay());
	}
	
	public void OnPointerExit(PointerEventData eventData)
	{
		StopCoroutine(TooltipDelayCoroutine);
        TooltipHandler.GetToolTipString("");
	}
    
    IEnumerator TooltipDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (this.gameObject.TryGetComponent(out TooltipStringInterface textInterface))
        {
            TooltipHandler.GetToolTipString(textInterface.GetTooltipString());
        }
    }
}
