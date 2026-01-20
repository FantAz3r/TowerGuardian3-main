using TMPro;
using UnityEngine;

public class DamageNumberSpawner : BaseSpawner
{
    private SpawnerType _type = SpawnerType.Text;

    private ObjectPool<DamageText> _pool;
    private int _startPoolSize = 0;
    private Vector3 _offset = new Vector3(0, 1.5f, -1.5f);

    public override SpawnerType GetSpawnerType() { return _type; }

    public DamageNumberSpawner(DamageText prefab)
    {
        _pool = new ObjectPool<DamageText>(prefab, _startPoolSize, true);
    }

    public override void Spawn(Vector3 position, int damage, Color? textColor = null)
    {
        if (CanSpawn == false)
            return;

        DamageText damageText = _pool.Get();
        damageText.transform.position = position + _offset;
        TMP_Text tmpText = damageText.GetComponentInChildren<TMP_Text>();

        tmpText.text = damage.ToString();
        tmpText.fontSharedMaterial.renderQueue = 4000;
        tmpText.color = textColor ?? Color.white;
    }

    public override void DestroyPool()
    {
        _pool.DestroyPool();
    }
}
