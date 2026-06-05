using System;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.Infrastructure.Servises
{
    public class MobileInput : IInputService
    {
        private Joystick _joystick;

        public event Action<Vector2> MovePerformed;
        public event Action<Vector2> RotateDirectionSeted;

        public void Init(Joystick joystick)
        {
            _joystick = joystick;
            _joystick.MovePerformed += OnMovePerformed;
            _joystick.MovePerformed += OnRotatePerformed;
        }

        private void OnMovePerformed(Vector2 direction)
        {
            MovePerformed?.Invoke(direction);
        }

        private void OnRotatePerformed(Vector2 direction)
        {
            RotateDirectionSeted?.Invoke(direction);
        }

        public void DisableInput()
        {
        }

        public void EnableInput()
        {
        }

        public void Dispose()
        {
            if (_joystick == null)
                return;

            _joystick.MovePerformed -= OnMovePerformed;
            _joystick.MovePerformed -= OnRotatePerformed;
        }

        public IInputService GetSelf()
        {
            return this;
        }
    }
}