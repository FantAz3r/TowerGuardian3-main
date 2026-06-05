using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Projectiles
{
    public class LavaRock : Projectile
    {
        private float _damage;
        private bool _hasDamagedPlayer;

        public void Init(float damage)
        {
            _damage = damage;
        }

        public override void Appear()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerHealth health) && !_hasDamagedPlayer)
            {
                health.TakeDamage(_damage);
                _hasDamagedPlayer = true;
                gameObject.SetActive(false);
            }
        }
    }
}
