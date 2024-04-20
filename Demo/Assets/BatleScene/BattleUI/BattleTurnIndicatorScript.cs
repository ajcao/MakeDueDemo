using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements.Experimental;

public class BattleTurnIndicatorScript : MonoBehaviour
{
    public TextMeshProUGUI TurnText;
    private Coroutine Flashing;

    // Update is called once per frame
    public void FlashTurn(bool PlayerTurn)
    {
        TurnText.color = new Color(0.0f, 0.0f, 0.0f, 0f);

        if (PlayerTurn)
            TurnText.text = "Player Turn";
        else
            TurnText.text = "Enemy Turn";

        if (Flashing != null)
        {
            Debug.Log("Turn Interuptted");
            StopCoroutine(Flashing);
            Flashing = null;
        }
        Flashing = StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        for (int i = 0; i < 30; i++)
        {
            TurnText.color += new Color(0.00f, 0.0f, 0.0f, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 30; i++)
        {
            TurnText.color -= new Color(0.00f, 0.00f, 0.0f, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        Flashing = null;
    }
}

