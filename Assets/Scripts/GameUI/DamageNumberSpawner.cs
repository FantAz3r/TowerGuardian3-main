using TMPro;
using UnityEngine;

public class DamageNumberSpawner : ISpawner
{
    private SpawnerType _type = SpawnerType.Text;

    private ObjectPool<DamageText> _pool;
    private int _startPoolSize = 0;
    private Vector3 _offset = new Vector3(0, 1.5f, -1.5f);
    private bool _spawning = true;

    public SpawnerType GetSpawnerType() { return _type; }

    public DamageNumberSpawner(DamageText prefab)
    {
        _pool = new ObjectPool<DamageText>(prefab, _startPoolSize, true);
    }

    public void DisableSpawn()
    {
        _spawning = false;
    }

    public void EnableSpawn()
    {
        _spawning = true;
    }

    public void Spawn(EntityType type, Vector3 position, int damage)
    {
        if (_spawning == false)
            return;

        DamageText damageText = _pool.Get();
        damageText.transform.position = position + _offset;
        TMP_Text tmpText = damageText.GetComponentInChildren<TMP_Text>();

        tmpText.text = damage.ToString();
        tmpText.fontSharedMaterial.renderQueue = 4000;
    }

    public void DestroyPool()
    {
        _pool.DestroyPool();
    }
}
