using UnityEngine;

[RequireComponent(typeof(Rotator))]
public class PlayerMover : MonoBehaviour
{
    private Mover _mover;
    private Rotator _rotator; 
    private IInputService _inputService;

    public void Init(IInputService inputService)
    {
        _inputService = inputService;

        _inputService.MovePerformed += OnMove;
        _inputService.MoveCanceled += OnMoveCanseled;
    }

    private void Awake()
    {
        _mover = GetComponentInParent<Mover>();
        _rotator = GetComponent<Rotator>();
    }

    private void OnDestroy()
    {
        _inputService.MovePerformed -= OnMove;
        _inputService.MoveCanceled -= OnMoveCanseled;
    }

    private void OnMove(Vector2 direction)
    {
        _mover.SetDirection(direction);
        _rotator.SetDirection(direction);
    }

    private void OnMoveCanseled()
    {
        _mover.SetDirection(Vector2.zero);
        _rotator.SetDirection(Vector2.zero);
    }
}
