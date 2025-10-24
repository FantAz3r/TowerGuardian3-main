using System;
using UnityEngine;

public interface IInputService : IService
{
    event Action<Vector2> MovePerformed;
    event Action MoveCanceled;

    event Action AttackPerformed;
    event Action AttackCanceled;

    event Action<Vector2> RotatePerformed;
    event Action<Vector2> RotateCanceled;
    event Action<Vector2> DirectionFromCursor;

    void EnableInput();
    void DisableInput();
}