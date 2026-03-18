using System.Collections.Generic;
using UnityEngine;

public interface ISceneContainer 
{
    List<Portal> Portals { get; }
    List<SpawnerActivator> SpawnPoints { get; }
    List<PlayerSpawnPoint> PlayerSpawnPoints { get; }
    List<GameObject> QuestObjects { get; }
}
