using System.Collections.Generic;
using TowerGuardian.StaticData;
using UnityEngine;

[CreateAssetMenu(fileName = "CostResourceData", menuName = "Configs/ResourceCostDatas")]

public class CostResourceData : ScriptableObject
{
    public List<PieceConfig> PieceConfigs = new List<PieceConfig>();
}
