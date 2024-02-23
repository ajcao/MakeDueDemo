using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemUtil
{

public static class ItemPool
{
    private static List<GameItem> ReturnList;

    public static GameItem PullRandomItem()
    {
        int r = Random.Range(0, ReturnList.Count);
        GameItem P = ReturnList[r];
        ReturnList.Remove(P);

        return P;
        
        
    }

    public static void GenerateNewitemPool()
    {
        ReturnList = new List<GameItem>();

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

        A = new BambooBerryItem();
        ReturnList.Add(A);

        A = new BatteryItem();
        ReturnList.Add(A);

        A = new HammerTridentItem();
        ReturnList.Add(A);

        A = new LawBookItem();
        ReturnList.Add(A);
        
        A = new HardhatItem();
        ReturnList.Add(A);
        
        A = new WeightsItem();
        ReturnList.Add(A);
        
        A = new WarBannerItem();
        ReturnList.Add(A);
        
        A = new MusicalHammerItem();
        ReturnList.Add(A);
        
        A = new IronSupplementItem();
        ReturnList.Add(A);
        
        A = new SoulContractItem();
        ReturnList.Add(A);
        
        A = new MindContractItem();
        ReturnList.Add(A);
        
        A = new PerpetualPendulumItem();
        ReturnList.Add(A);
    }

}

}
