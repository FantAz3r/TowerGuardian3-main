using System.Collections.Generic;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Configs.Interfaces
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