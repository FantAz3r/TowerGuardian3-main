using System.Collections.Generic;
using UnityEngine;

public abstract class ShopConfig : ScriptableObject, IShopConfig
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected List<CostInfo> _costs = new List<CostInfo>();

    public Sprite Icon => _icon;
    public string Name => _name;
    public string Description => _description;

    public IReadOnlyList<CostInfo> Costs => _costs;
    public virtual List<CostInfo> GetCosts() => _costs;
    public virtual List<CostInfo> GetSellCost() { return null; }
}
