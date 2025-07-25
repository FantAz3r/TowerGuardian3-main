using System;
using UnityEngine;

public interface IInputService : IService
{
    event Action<Vector2> MovePerformed;
    event Action MoveCanceled;

    event Action AttackPerformed;
    event Action AttackCanceled;

    void EnableInput();

    void DisableInput();
}