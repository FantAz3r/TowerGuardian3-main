using System.Collections;
using System.Collections.Generic;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Effects;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.StaticData.Datas;
using UnityEngine;

namespace TowerGuardian.Scripts.Spawners
{
    public class EffectSpawner : BaseSpawner
    {
        private EffectData _data;
        private Dictionary<EffectType, ObjectPool<Effect>> _pools = new Dictionary<EffectType, ObjectPool<Effect>>();

        public EffectSpawner(EffectData data)
        {
            _data = data;

            foreach (var effect in data.EffectInfos)
            {
                _pools.Add(effect.EffectType, new ObjectPool<Effect>(effect.Prefab, 0, true));
            }
        }

        public override SpawnerType GetSpawnerType() => SpawnerType.Effects;

        public override void Spawn(EffectType type, Vector3 position, Transform parent = null)
        {
            if (!CanSpawn)
            {
                return;
            }

            if (!_pools.TryGetValue(type, out var pool))
            {
                return;
            }

            Effect effect = pool.Get();

            if (parent != null)
            {
                effect.transform.position = position;
                effect.transform.SetParent(parent);
            }
            else
            {
                effect.transform.position = position + _data.GetEffectInfo(type).Offset;
            }

            effect.gameObject.SetActive(true);

            if (effect.TryGetComponent(out ParticleSystem particle))
            {
                particle.Play();
                effect.GetComponent<MonoBehaviour>().StartCoroutine(ReturnToPoolAfterDuration(effect, pool, particle.main.duration));
            }
        }

        public override void DestroyPool()
        {
            foreach (var pair in _pools)
            {
                pair.Value.DestroyPool();
            }
        }

        private IEnumerator ReturnToPoolAfterDuration(Effect effect, ObjectPool<Effect> pool, float delay)
        {
            yield return new WaitForSeconds(delay);
            effect.gameObject.SetActive(false);
        }
    }
}
