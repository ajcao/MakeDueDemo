using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyMoveUtil;
using EnemyTargetingLibraryUtil;
using TMPro;
using UnityEngine.UI;

public class EnemyMoveIndicatorScript : MonoBehaviour
{
    EnemyMove EM;
    
    
    public void Init(EnemyMove InputEM)
    {
        EM = InputEM;
        this.gameObject.transform.Find("EnemyMoveSprite").gameObject.GetComponent<Image>().sprite = EM.getIcon();
        EM.MoveTargetIndicatorText(this.gameObject, EM.getTargetArray());
    }
    
    public void Clean()
    {
        EM = null;
        Destroy(this.gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        TextMeshPro text = this.gameObject.transform.Find("EnemyMoveText").gameObject.GetComponent<TextMeshPro>();
        text.text = EM.MoveIndicatorText();
        
        
    }
}
