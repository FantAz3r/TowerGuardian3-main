using System.Collections.Generic;
using UnityEngine;

public class SpawnPointContainer : MonoBehaviour
{
    [SerializeField] private List<SpawnerActivator> _spawnPoints;

    public IReadOnlyList<SpawnerActivator> SpawnPoints => _spawnPoints;
}
