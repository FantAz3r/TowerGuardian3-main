using System.Collections.Generic;
using TowerGuardian.Scripts.GamePlayElements.Entity.Enemies;
using TowerGuardian.Scripts.GamePlayElements.Envitoment;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using UnityEngine;

namespace TowerGuardian.Scripts.Infrastructure.Servises.Interfaces
{
    public interface ISceneContainer
    {
        List<Portal> Portals { get; }

        List<SpawnerActivator> SpawnPoints { get; }

        List<PlayerSpawnPoint> PlayerSpawnPoints { get; }

        List<GameObject> QuestObjects { get; }
    }
}
