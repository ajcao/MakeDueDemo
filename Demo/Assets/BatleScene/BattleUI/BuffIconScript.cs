using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffUtil;
using TMPro;
using TooltipUtil;
using Unity.VisualScripting;

public class BuffIconScript : MonoBehaviour, TooltipStringInterface
{
    Buff B;
    public TextMeshPro Intensity;
    public TextMeshPro Duration;

    public Coroutine Flashing;

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

    public void ToggleFlashing(bool b)
    {
        if (b && (Flashing == null))
        {
            Flashing = StartCoroutine(FlashingBar());
        }
        else if (!b && (Flashing != null))
        {
            StopCoroutine(Flashing);
            Flashing = null;
        }
    }

    IEnumerator FlashingBar()
    {
        while (true)
        {
            for (int i = 0; i < 80; i++)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color -= new Color(0.00f, 0.00f, 0.01f, 0.00f);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.05f);

            for (int i = 0; i < 80; i++)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color += new Color(0.00f, 0.00f, 0.01f, 0.00f);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    public string GetTooltipString()
    {
        return B.GetTooltipString();
    }
}
