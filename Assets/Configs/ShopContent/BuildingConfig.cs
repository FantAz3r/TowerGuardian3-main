using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Goods", menuName = "Configs/product")]

public class BuildingConfig : ShopConfig
{
    [SerializeField] private BuildingObject _prefab;

    public BuildingObject Prefab => _prefab;
}
