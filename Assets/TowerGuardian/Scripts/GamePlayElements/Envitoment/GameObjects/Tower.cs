using System.Collections.Generic;
using TowerGuardian.Scripts.GamePlayElements.Entity.Enemies;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects
{
    public class Tower : MonoBehaviour, ISceneContainer
    {
        [field: SerializeField] public Platform ShopPlatform { get; private set; }
        [field: SerializeField] public TowerDoor Door { get; private set; }
        [field: SerializeField] public StairsTrigger StairsFirstFloor { get; private set; }
        [field: SerializeField] public TowerRenderer TowerRenderer { get; private set; }
        [field: SerializeField] public List<Portal> Portals { get; private set; }
        [field: SerializeField] public List<PlayerSpawnPoint> PlayerSpawnPoints { get; private set; }
        [field: SerializeField] public List<GameObject> QuestObjects { get; private set; }

        public List<SpawnerActivator> SpawnPoints => null;
    }
}
