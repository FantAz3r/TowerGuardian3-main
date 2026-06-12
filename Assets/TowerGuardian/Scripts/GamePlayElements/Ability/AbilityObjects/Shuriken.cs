using TowerGuardian.Scripts.GamePlayElements.Entity;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Ability.AbilityObjects
{
    public class Shuriken : MonoBehaviour
    {
        private int _damage;
        private float _speenSpeed;
        private bool _isRotate = true;

        private void OnDisable()
        {
            _isRotate = false;
        }

        public void SetParametrs(int damage, float speenSpeed)
        {
            _damage = damage;
            _speenSpeed = speenSpeed;
            _isRotate = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Health demageable))
            {
                int damage = (int)Mathf.Min(_damage, demageable.CurrentHealth);
                demageable.TakeDamage(damage);
            }
        }

        private void Update()
        {
            if (_isRotate)
            {
                transform.Rotate(0, _speenSpeed * Time.deltaTime, 0);
            }
        }
    }
}
