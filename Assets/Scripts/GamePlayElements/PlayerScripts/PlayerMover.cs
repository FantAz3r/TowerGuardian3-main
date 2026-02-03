using System;
using UnityEngine;

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
        _inputService.MoveCanceled += OnMoveCanceled;
        _inputService.DirectionFromCursor += OnRotate;
    }

    private void OnDestroy()
    {
        if (_inputService != null)
        {
            _inputService.MovePerformed -= OnMove;
            _inputService.MoveCanceled -= OnMoveCanceled;
            _inputService.DirectionFromCursor -= OnRotate;
        }
    }

    private void OnMove(Vector2 direction)
    {
        _mover.SetDirection(direction);

        if (direction != Vector2.zero)
            Moved?.Invoke();
    }

    private void OnMoveCanceled()
    {
        _mover.SetDirection(Vector2.zero);
    }

    private void OnRotate(Vector2 direction)
    {
        _rotator.SetDirection(direction);
    }
}
