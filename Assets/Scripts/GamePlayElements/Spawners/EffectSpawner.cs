using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : BaseSpawner
{
    private Vector3 _offset = new Vector3(0, 1, 0);
    private Dictionary<EffectType, ObjectPool<Effect>> _pools = new Dictionary<EffectType, ObjectPool<Effect>>();

    public override SpawnerType GetSpawnerType() => SpawnerType.Effects;

    public EffectSpawner(EffectData data)
    {
        foreach (var effect in data.EffectInfos)
        {
            _pools.Add(effect.EffectType, new ObjectPool<Effect>(effect.Prefab, 0, true));
        }
    }

    public override void Spawn(HealthConfig config, Vector3 position)
    {
        if (CanSpawn == false)
            return;

        if (_pools.TryGetValue(config.SpawnEffect, out var pool) == false)
        {
            Debug.LogWarning($"Pool for effect type {config.SpawnEffect} not found");
            return;
        }

        Effect effect = pool.Get();
        effect.gameObject.SetActive(true);
        effect.transform.position = position + _offset;

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
