using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Ability.AbilityInfrastructure;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Configs
{
    public abstract class AbilityConfig : CardConfig
    {
        [field: SerializeField]
        public Ability Prefab { get; private set; }

        [field: SerializeField]
        public AbilityType AbilityType { get; private set; }

        public override CardType GetCardType() => CardType.Ability;
    }
}