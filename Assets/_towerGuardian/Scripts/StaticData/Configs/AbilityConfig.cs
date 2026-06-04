using TowerGuardian.Enums;
using UnityEngine;

namespace TowerGuardian.StaticData
{
    public abstract class AbilityConfig : CardConfig
    {
        [field: SerializeField] public Ability Prefab { get; private set; }
        [field: SerializeField] public AbilityType AbilityType { get; private set; }

        public override CardType GetCardType() => CardType.Ability;
    }
}