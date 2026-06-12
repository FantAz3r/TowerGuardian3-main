using System;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.PlayerScripts
{
    [RequireComponent(typeof(Rotator))]
    public class PlayerMover : MonoBehaviour
    {
        private Mover _mover;
        private Rotator _rotator;
        private IInputService _inputService;

        public event Action Moved;

        private void Awake()
        {
            _mover = GetComponentInParent<Mover>();
            _rotator = GetComponent<Rotator>();
            _inputService = ServiceLocator.Get<IInputService>();

            _inputService.MovePerformed += OnMove;
            _inputService.RotateDirectionSeted += OnRotate;
        }

        private void OnDestroy()
        {
            if (_inputService != null)
            {
                _inputService.MovePerformed -= OnMove;
                _inputService.RotateDirectionSeted -= OnRotate;
            }
        }

        private void OnMove(Vector2 direction)
        {
            _mover.SetDirection(direction);

            if (direction != Vector2.zero)
            {
                Moved?.Invoke();
            }
        }

        private void OnRotate(Vector2 direction)
        {
            _rotator.SetDirection(direction);
        }
    }
}
