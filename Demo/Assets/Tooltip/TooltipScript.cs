using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TooltipUtil;
using UnityEngine.EventSystems;

//To be place on GameObject prefab that should output a tooltip when hovered over
//EX: buff, abilities, items, etc.
//Should be placed on each prefab
public class TooltipScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    private Coroutine TooltipDelayCoroutine;
    
	public void OnPointerEnter(PointerEventData eventData)
	{
		TooltipDelayCoroutine = StartCoroutine(TooltipDelay());
	}
	
	public void ResetToolTip()
	{
		if (TooltipDelayCoroutine != null)
		{
			StopCoroutine(TooltipDelayCoroutine);
			TooltipHandler.GetToolTipString("");
		}
	}
	
	public void OnPointerExit(PointerEventData eventData)
	{
		this.ResetToolTip();
	}
	
	public void OnDestroy()
	{
		this.ResetToolTip();
	}
	
	public void OnDisable()
	{
		this.ResetToolTip();
	}
    
    IEnumerator TooltipDelay()
    {
        yield return new WaitForSeconds(0.5f);
		while (true)
		{
			if (this.gameObject.TryGetComponent(out TooltipStringInterface textInterface))
			{
				TooltipHandler.GetToolTipString(textInterface.GetTooltipString());
			}
			yield return null;
		}
    }
}
