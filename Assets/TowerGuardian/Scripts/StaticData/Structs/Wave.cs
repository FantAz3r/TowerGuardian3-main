using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TowerGuardian.Scripts.GamePlayElements.Entity.Enemies;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Structs
{
    [Serializable]
    public struct Wave
    {
        [SerializeField]
        private SerializedDictionary<Enemy, int> _weight;

        [field: SerializeField]
        [field: Min(1f)]
        public float Duration { get; private set; }

        [field: SerializeField]
        [field: Min(0.1f)]
        public float NightSpawnDelay { get; private set; }

        [field: SerializeField]
        [field: Min(0.1f)]
        public float DaySpawnDelay { get; private set; }

        [field: SerializeField]
        [field: Min(1f)]
        public int MaxEnemyCount { get; private set; }

        public IReadOnlyDictionary<Enemy, int> Weight => _weight;
    }
}