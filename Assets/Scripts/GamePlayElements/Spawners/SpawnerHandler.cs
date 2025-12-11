using System;
using System.Collections.Generic;
using UnityEngine;

//public class SpawnerHandler : MonoBehaviour
//{
//    private Dictionary<ResourceType, SpawnbleObjectSpawner<ResourcePiece>> _resourceSpawners = new Dictionary<ResourceType, SpawnbleObjectSpawner<ResourcePiece>>();
//    private Dictionary<EnemyType, SpawnbleObjectSpawner<Enemy>> _enemySpawners = new Dictionary<EnemyType, SpawnbleObjectSpawner<Enemy>>();
//
//    private Dictionary<EnemyType, EnemyConfig> _enemiesData = new Dictionary<EnemyType, EnemyConfig>();
//
//    private TReturn Spawn<TKey, TReturn>(
//    Dictionary<TKey, SpawnbleObjectSpawner<TReturn>> spawners,
//    TKey reqestbleType,
//    Vector3 position,
//    Action<TReturn> initialCallBack)
//    where TReturn : MonoBehaviour, ISpawnbleObject<TReturn>
//    {
//        if (spawners.TryGetValue(reqestbleType, out var spawner))
//        {
//            var spawnbleObject = spawner.EnableObject(position);
//
//            if (spawnbleObject != null)
//            {
//                initialCallBack(spawnbleObject);
//            }
//
//            return spawnbleObject;
//        }
//
//        return null;
//    }
//
//    public Enemy SpawnEnemy(EnemyType reqestedType, List<ElementType> reqestedElement, Vector3 position)
//    {
//        _enemiesData.TryGetValue(reqestedType, out EnemyConfig config);
//
//        return Spawn(
//            _enemySpawners,
//            reqestedElement,
//            position
//            );
//    }
//}
//