using System;
using UnityEngine;

public interface IInputService : IService
{
    event Action<Vector2> MovePerformed;
    event Action MoveCanceled;

    event Action OnAbillity1Used;
    event Action OnAbillity2Used;
    event Action OnAbillity3Used;
    event Action OnAbillity4Used;

    event Action<Vector2> RotatePerformed;
    event Action<Vector2> RotateCanceled;
    event Action<Vector2> DirectionFromCursor;
    void EnableInput();
    void DisableInput();
}