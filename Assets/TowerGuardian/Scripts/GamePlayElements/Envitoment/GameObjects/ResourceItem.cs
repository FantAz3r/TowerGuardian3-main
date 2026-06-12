using System.Collections;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Entity;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Factories;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ResourceItem : MonoBehaviour
    {
        private const int NoGameLevelConstant = 4;
        private Health _health;
        private LevelID _currentLevel = LevelID.None;
        private WaitForSeconds _onesecond = new WaitForSeconds(1.5f);

        private void Start()
        {
            _health = GetComponent<Health>();

            StartCoroutine(WaitRoutine());
        }

        private IEnumerator WaitRoutine()
        {
            yield return _onesecond;
            _currentLevel = ServiceLocator.Get<IGameFactory>().LevelConfig.Level;

            _health.Config.SetLevel(Mathf.Max(0, (int) _currentLevel - NoGameLevelConstant));
            _health.Init(_health.Config.GetMaxHealth());
        }
    }
}
