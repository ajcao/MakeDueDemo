using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemUtil
{
    
public class EntireLibraryItem : MonoBehaviour
{    
    public List<GameItem> GetAllItems()
    {
        List<GameItem> ReturnList = new List<GameItem>();
        
        GameItem A;
        
        A = new ApaxeItem();
        ReturnList.Add(A);
        
        A = new ParryShieldItem();
        ReturnList.Add(A);
        
        A = new MindAmuletItem();
        ReturnList.Add(A);
        
        
        return ReturnList;
    }
}

}
