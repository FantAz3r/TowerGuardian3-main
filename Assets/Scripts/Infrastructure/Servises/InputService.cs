using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : IInputService
{
    private PlayerInputActions _inputActions;

    public event Action<Vector2> MovePerformed;
    public event Action MoveCanceled;

    public event Action AttackPerformed;
    public event Action AttackCanceled;

    public InputService()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();

        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;

        _inputActions.Player.Attack.performed += OnAttackPerformed;
        _inputActions.Player.Attack.canceled += OnAttackCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        MovePerformed?.Invoke(direction);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveCanceled?.Invoke();
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        AttackPerformed?.Invoke();
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        AttackCanceled?.Invoke();
    }

    public void EnableInput()
    {
        _inputActions.Player.Enable();
    }

    public void DisableInput()
    {
        _inputActions.Player.Disable();
    }

    public void Dispose()
    {
        if (_inputActions == null)
            return;

        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;

        _inputActions.Player.Attack.performed -= OnAttackPerformed;
        _inputActions.Player.Attack.canceled -= OnAttackCanceled;

        _inputActions.Dispose();
        _inputActions = null;
    }
}
