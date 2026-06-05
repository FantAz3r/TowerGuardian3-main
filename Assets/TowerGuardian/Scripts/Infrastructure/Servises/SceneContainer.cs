using System.Collections.Generic;
using TowerGuardian.Scripts.GamePlayElements.Entity.Enemies;
using TowerGuardian.Scripts.GamePlayElements.Envitoment;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.Infrastructure.Servises
{
    public class SceneContainer : MonoBehaviour, ISceneContainer
    {
        [field: SerializeField] public List<Portal> Portals { get; private set; }
        [field: SerializeField] public List<SpawnerActivator> SpawnPoints { get; private set; }
        [field: SerializeField] public List<PlayerSpawnPoint> PlayerSpawnPoints { get; private set; }
        [field: SerializeField] public List<GameObject> QuestObjects { get; private set; }
    }
}