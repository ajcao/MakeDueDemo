using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using CharacterUtil;

namespace EnemyTargetingLibraryUtil
{
    
public static class EnemyTargetingLibrary
{
    public static Character[] TargetNRandomHeroes(int inputN)
    {
        int N = Mathf.Min(inputN, PlayerParty.getPartySize());
        List<Character> randomList = new List<Character>();
        Character[] Targets = new Character[N];
        foreach (GameObject G in PlayerParty.getParty())
        {
            randomList.Add((Character) G.GetComponent<PlayableCharacter>());
        }
        int i = 0;
        while (randomList.Count > 0 && i < N)
        {
            int r = Random.Range(0,randomList.Count);
            Character T = randomList[r];
            Targets[i] = T;
            randomList.Remove(T);
            i++;
        }
        
        return Targets;
    }
    
    public static void TargetNGenericIndicator(GameObject targetCanvas, Character[] CList)
    {
        for (int i = 0; i < CList.Length; i++)
        {
            Character C = CList[i];
            GameObject ImageObject = new GameObject("Targeting" + C.ToString());
            Image CImage = ImageObject.AddComponent<Image>();
            CImage.sprite = C.getCharacterIcon();
            ImageObject.transform.SetParent(targetCanvas.transform);
            ImageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f, 0.5f);
            ImageObject.GetComponent<RectTransform>().transform.position = new Vector3((-0.5f+0.5f*i),-0.6f,0.0f) + targetCanvas.transform.position;
        }
    }


}

}