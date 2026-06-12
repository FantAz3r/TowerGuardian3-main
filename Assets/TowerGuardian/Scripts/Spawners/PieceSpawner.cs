using System.Collections.Generic;
using DG.Tweening;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.StaticData.Configs.EntityConfigs;
using TowerGuardian.Scripts.StaticData.Datas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerGuardian.Scripts.Spawners
{
    public class PieceSpawner : BaseSpawner
    {
        private readonly float _ejectForceMin = 5f;
        private readonly float _ejectForceMax = 10f;
        private readonly float _ejectRadius = 2f;

        private Dictionary<ResourceType, ObjectPool<ResourcePiece>> _pools = new Dictionary<ResourceType, ObjectPool<ResourcePiece>>();
        private ResourceData _data;

        public PieceSpawner(ResourceData data)
        {
            _data = data;

            foreach (var resource in _data.ResourceInfos)
            {
                if (!_pools.ContainsKey(resource.Type))
                {
                    _pools.Add(resource.Type, new ObjectPool<ResourcePiece>(resource.Prefab, 0, true));
                }
            }
        }

        public override SpawnerType GetSpawnerType() => SpawnerType.Resources;

        public override void Spawn(HealthConfig config, Vector3 position, int count = 0)
        {
            if (!CanSpawn)
            {
                return;
            }

            if (!_pools.TryGetValue(config.SpawnResource, out var pool))
            {
                return;
            }

            float rootDegree = 2.5f;
            int spawnCount = Mathf.CeilToInt(Mathf.Pow(count, 1f / rootDegree));
            int pointsPerObject = Mathf.CeilToInt((float) count / spawnCount);

            for (int i = 0; i < spawnCount; i++)
            {
                ResourcePiece piece = pool.Get();
                Vector3 startPos = CreateSpawnPoint(position);
                piece.transform.position = startPos;

                Vector3 ejectDirection = Random.onUnitSphere;
                ejectDirection.y = Mathf.Abs(ejectDirection.y);
                ejectDirection.Normalize();

                float distance = Random.Range(_ejectForceMin, _ejectForceMax);
                Vector3 endPos = startPos + (ejectDirection * distance);

                float jumpPower = distance * 0.3f;
                float duration = 0.4f;

                piece.SetAmount(pointsPerObject);

                piece.transform.DOJump(endPos, jumpPower, 1, duration)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        piece.GetComponent<Collider>().enabled = true;
                    });
            }
        }

        public override void DestroyPool()
        {
            foreach (var pair in _pools)
            {
                pair.Value.DestroyPool();
            }
        }

        private Vector3 CreateSpawnPoint(Vector3 origin)
        {
            Vector3 spawnPos = origin + (Random.insideUnitSphere * _ejectRadius);
            spawnPos.y = origin.y;
            return spawnPos;
        }
    }
}
