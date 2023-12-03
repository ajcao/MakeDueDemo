using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemUtil
{
    
public static class EntireLibraryItem
{
    
    public static List<GameItem> GetAllItems()
    {
        List<GameItem> ReturnList = new List<GameItem>();
        
        GameItem A;
        
        A = new ApaxeItem();
        ReturnList.Add(A);
        
        A = new ParryShieldItem();
        ReturnList.Add(A);
        
        A = new MindAmuletItem();
        ReturnList.Add(A);
        
        A = new EnergyPillsItem();
        ReturnList.Add(A);
        
        A = new LeftScarabShellItem();
        ReturnList.Add(A);
        
        A = new RightScarabShellItem();
        ReturnList.Add(A);
        
        A = new ScarabHeadItem();
        ReturnList.Add(A);
        
        A = new SimpleStoneItem();
        ReturnList.Add(A);
        
        A = new MosquitoHeadItem();
        ReturnList.Add(A);
        
        A = new DemonCubeItem();
        ReturnList.Add(A);
        
        A = new HeavensSpearItem();
        ReturnList.Add(A);
        
        A = new BlessedWineItem();
        ReturnList.Add(A);
        
        A = new MagicMissileCannedItem();
        ReturnList.Add(A);
        
        A = new BluePhilospherStoneItem();
        ReturnList.Add(A);
        
        A = new ScrollOfKnivesItem();
        ReturnList.Add(A);
        
        A = new ArmorRepairKitItem();
        ReturnList.Add(A);
        
        A = new ShinobiKatanaItem();
        ReturnList.Add(A);
        
        A = new LivingWoodArmorItem();
        ReturnList.Add(A);
        
        A = new MetalRoseItem();
        ReturnList.Add(A);
        
        A = new BrainSlugItem();
        ReturnList.Add(A);
        
        return ReturnList;
    }
}

}
