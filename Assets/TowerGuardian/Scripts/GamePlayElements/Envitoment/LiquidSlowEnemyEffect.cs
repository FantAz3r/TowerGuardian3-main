using TowerGuardian.Scripts.GamePlayElements.Buffs.StatsCalculator;
using TowerGuardian.Scripts.GamePlayElements.Entity.Enemies;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment
{
    public class LiquidSlowEnemyEffect : MonoBehaviour
    {
        [SerializeField] private float _slowValue = 0.5f;
        private MultiplyEffect _effect;

        private void Awake()
        {
            _effect = new MultiplyEffect(_slowValue);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyMover health))
            {
                health.ApplyBuff(_effect);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out EnemyMover health))
            {
                health.RemoveBuff(_effect);
            }
        }
    }
}
