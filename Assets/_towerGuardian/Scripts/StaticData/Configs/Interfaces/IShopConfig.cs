using System.Collections.Generic;
using UnityEngine;

namespace TowerGuardian.StaticData
{
    public interface IShopConfig
    {
        string ID { get; }
        Sprite Icon { get; }
        string Name { get; }
        string Description { get; }
        List<CostInfo> GetCosts();
        List<CostInfo> GetSellCosts();
    }
}