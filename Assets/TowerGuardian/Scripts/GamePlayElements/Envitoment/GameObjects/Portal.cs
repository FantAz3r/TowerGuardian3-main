using System;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private LevelID _nextLevel;
        [SerializeField] private LevelID _currentLevel;

        private bool _canExit = true;
        private IGameConditionService _conditionService;

        public event Action Entered;
        public LevelID NextLevel => _nextLevel;

        private void Awake()
        {
            _conditionService = ServiceLocator.Get<IGameConditionService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_canExit)
                return;

            if (other.TryGetComponent<Player>(out _))
            {
                if (_currentLevel == LevelID.Tower)
                {
                    _conditionService.OnStart(this);
                }
                else
                {
                    _conditionService.OnWin();
                }

                Entered?.Invoke();
            }
        }

        public void CanExit(bool canExit)
        {
            _canExit = canExit;
        }
    }
}
