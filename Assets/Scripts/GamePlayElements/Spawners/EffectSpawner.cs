using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : ISpawner
{
    private Vector3 _offset = new Vector3(0, 1, 0);
    private Dictionary<EffectType, ObjectPool<Effect>> _pools = new Dictionary<EffectType, ObjectPool<Effect>>();
    private bool _spawning = true;

    public SpawnerType GetSpawnerType() => SpawnerType.Effects;

    public EffectSpawner(EffectData data)
    {
        foreach (var effect in data.EffectInfos)
        {
            _pools.Add(effect.EffectType, new ObjectPool<Effect>(effect.Prefab, 0, true));
        }
    }

    public void EnableSpawn()
    {
        _spawning = true;
    }

    public void DisableSpawn()
    {
        _spawning = false;
    }

    public void Spawn(HealthConfig config, Vector3 position, int count = 1)
    {
        if (_spawning == false)
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

    private IEnumerator ReturnToPoolAfterDuration(Effect effect, ObjectPool<Effect> pool, float delay)
    {
        yield return new WaitForSeconds(delay);
        effect.gameObject.SetActive(false);
    }

    public void DestroyPool()
    {
        foreach (var pair in _pools)
        {
            pair.Value.DestroyPool();
        }
    }

    public void Spawn(EntityType type, Vector3 position, int count)
    {
        throw new System.NotImplementedException();
    }
}
