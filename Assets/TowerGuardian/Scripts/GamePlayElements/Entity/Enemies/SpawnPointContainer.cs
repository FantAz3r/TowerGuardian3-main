using System.Collections.Generic;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Entity.Enemies
{
    public class SpawnPointContainer : MonoBehaviour
    {
        [SerializeField] private List<SpawnerActivator> _spawnPoints;

        public IReadOnlyList<SpawnerActivator> SpawnPoints => _spawnPoints;
    }
}
