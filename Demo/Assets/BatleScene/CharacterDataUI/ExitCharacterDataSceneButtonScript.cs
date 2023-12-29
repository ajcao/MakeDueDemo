using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitCharacterDataSceneButtonScript : MonoBehaviour
{
    public void onButtonClick()
    {
        Debug.Log("Going Back");
        
        //Destory Dummy Character
        GameObject DummyCharacter = GameObject.Find("DummyCharacterGameObject");
        Destroy(DummyCharacter);
        
        //Set Item Selection Screen UI to be visible again
        GameObject.Find("ButtonCanvas").GetComponent<Canvas>().enabled = true;
        
        //Move Characters back to right place
        PlayerParty.getPartyMember(0).transform.position = new Vector2(-6f,3.0f);
        PlayerParty.getPartyMember(1).transform.position = new Vector2(-2f,3.0f);
        PlayerParty.getPartyMember(2).transform.position = new Vector2(2f,3.0f);
        PlayerParty.getPartyMember(3).transform.position = new Vector2(6f,3.0f);
        
        SceneManager.UnloadSceneAsync("CharacterDataScene");
    }
}
