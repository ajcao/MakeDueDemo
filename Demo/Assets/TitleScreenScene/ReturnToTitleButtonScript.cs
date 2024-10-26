using CharacterUtil;
using ItemUtil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTitleButtonScript : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("TitleScreenScene", LoadSceneMode.Single);
    }
}
