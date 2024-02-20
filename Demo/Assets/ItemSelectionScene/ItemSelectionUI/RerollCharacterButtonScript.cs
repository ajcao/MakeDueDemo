using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollCharacterButtonScript : MonoBehaviour
{
    public ItemSelectionSceneHandler ISSH;

    public void OnButtonClick()
    {
        ISSH.GenerateRandomParty();
    }
}
