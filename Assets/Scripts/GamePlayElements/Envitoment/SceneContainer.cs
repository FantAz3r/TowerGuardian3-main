using System.Collections.Generic;
using UnityEngine;

public class SceneContainer : MonoBehaviour, ISceneContainer
{
    [field: SerializeField] public List<Portal> Portals { get; private set; }
    [field: SerializeField] public List<SpawnerActivator> SpawnPoints { get; private set; }
    [field: SerializeField] public List<PlayerSpawnPoint> PlayerSpawnPoints { get; private set; }
    [field: SerializeField] public List<GameObject> QuestObjects { get; private set; }

}
