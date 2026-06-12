using System;
using UnityEngine;

namespace TowerGuardian.Scripts.Infrastructure.Servises.Interfaces
{
    public interface IInputService : IService
    {
        event Action<Vector2> MovePerformed;

        event Action<Vector2> RotateDirectionSeted;

        IInputService GetSelf();

        void EnableInput();

        void DisableInput();
    }
}