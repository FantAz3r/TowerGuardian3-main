using System.Collections.Generic;
using TowerGuardian.Scripts.GamePlayElements.CardsInfrastructure;
using TowerGuardian.Scripts.Localization;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Configs.AbilityConfigs
{
    [CreateAssetMenu(menuName = "Abilities/JumpingPickaxeConfig")]

    public class JumpingPickaxeConfig : AbilityConfig
    {
        [SerializeField]
        private int _bouncesCount = 3;
        [SerializeField]
        private float _bounceRange = 8f;
        [SerializeField]
        private float _cooldownPerHit = 3f;
        [SerializeField]
        private float _flySpeed = 20;

        [SerializeField]
        private float _bouncePerLevel = 1;
        [SerializeField]
        private float _rangePerLevel = 1f;
        [SerializeField]
        private float _cooldownPerLevel = 0.2f;

        public float FlySpeed => _flySpeed;

        public int BouncesCount => Mathf.Max(_bouncesCount, _bouncesCount + (int) (_bouncePerLevel * (Level - 1)));

        public float BounceRange => Mathf.Max(_bounceRange, _bounceRange + (_rangePerLevel * (Level - 1)));

        public float CooldownPerHit => Mathf.Max(_cooldownPerHit, _cooldownPerHit - (_cooldownPerLevel * (Level - 1)));

        public override List<CardStats> GetStats()
        {
            return new List<CardStats>
        {
            new CardStats(UIText.BouncesCount, BouncesCount, Mathf.Max(_bouncesCount, _bouncesCount + (_bouncePerLevel * Level))),
            new CardStats(UIText.BounceRange, BounceRange, Mathf.Max(_bounceRange, _bounceRange + (_rangePerLevel * Level))),
            new CardStats(UIText.CooldownPerHit, CooldownPerHit, Mathf.Max(_cooldownPerHit, _cooldownPerHit - (_cooldownPerLevel * Level))),
        };
        }
    }
}