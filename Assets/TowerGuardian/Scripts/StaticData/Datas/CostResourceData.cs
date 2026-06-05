using System.Collections.Generic;
using TowerGuardian.Scripts.StaticData.Configs;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Datas
{
    [CreateAssetMenu(fileName = "CostResourceData", menuName = "Configs/ResourceCostDatas")]

    public class CostResourceData : ScriptableObject
    {
        public List<PieceConfig> PieceConfigs = new List<PieceConfig>();
    }
}