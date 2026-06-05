using TMPro;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.UI.EnviromentUI;
using UnityEngine;

namespace TowerGuardian.Scripts.Spawners
{
    public class DamageNumberSpawner : BaseSpawner
    {
        private SpawnerType _type = SpawnerType.Text;
        private ObjectPool<DamageText> _pool;
        private int _startPoolSize = 0;
        private Vector3 _offset = new Vector3(0, 1.5f, -1.5f);

        public DamageNumberSpawner(DamageText prefab)
        {
            _pool = new ObjectPool<DamageText>(prefab, _startPoolSize, true);
        }

        public override SpawnerType GetSpawnerType() => _type;

        public override void Spawn(Vector3 position, int damage, Color? textColor = null)
        {
            if (!CanSpawn)
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
}