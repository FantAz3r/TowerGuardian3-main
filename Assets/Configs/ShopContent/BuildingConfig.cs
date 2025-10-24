using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Goods", menuName = "Configs/product")]

public class BuildingConfig : ScriptableObject, IShopConfig
{
    [SerializeField] private BuildingObject _prefab;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private List<CostInfo> _costs = new List<CostInfo>();

    public BuildingObject Prefab => _prefab;
    public Sprite Icon => _icon;
    public string Name => _name;
    public string Description => _description;
    
    public List<CostInfo> GetCosts() => _costs;
}
