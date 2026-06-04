using System.Collections.Generic;
using TowerGuardian.Enums;
using UnityEngine;

namespace TowerGuardian.StaticData
{
    [CreateAssetMenu(fileName = "PieceConfig", menuName = "Configs/PieceConfig")]

    public class PieceConfig : ShopConfig
    {
        [SerializeField] private ResourceType _type;

        public ResourceType Type => _type;

        public override List<CostInfo> GetSellCosts()
        {
            List<CostInfo> sellCosts = new List<CostInfo>();
            float sellCoefficient = 0.5f;

            foreach (var info in GetCosts())
            {
                float newAmount = info.Value * sellCoefficient;
                sellCosts.Add(new CostInfo(info.ResourceType, Mathf.CeilToInt(newAmount)));
            }

            return sellCosts;
        }
    }
}