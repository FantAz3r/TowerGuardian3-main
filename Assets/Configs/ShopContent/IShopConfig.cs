using System.Collections.Generic;
using UnityEngine;

public interface IShopConfig
{
    Sprite Icon { get; }
    string Name { get; }
    string Description { get; }
    List<CostInfo> GetCosts();
    List<CostInfo> GetSellCost();
}


