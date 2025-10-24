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

    public event Action OnAbillityUsed;

    public event Action<Vector2> RotatePerformed;
    public event Action<Vector2> RotateCanceled;

    public event Action<Vector2> DirectionFromCursor;

    public Vector2 CursorOrigin { get; set; }

    public InputService()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();

        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;

        _inputActions.Player.Attack.performed += OnAttackPerformed;
        _inputActions.Player.Attack.canceled += OnAttackCanceled;

        _inputActions.UI.ActivateAbility.performed += OnAbilityUsed;

        _inputActions.Player.Rotate.performed += OnRotatePerformed;
        _inputActions.Player.Rotate.canceled += OnRotateCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        MovePerformed?.Invoke(direction);
    }

    private void OnAbilityUsed(InputAction.CallbackContext context)
    {
        OnAbillityUsed?.Invoke();
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

    private void OnRotatePerformed(InputAction.CallbackContext context)
    {
        CursorOrigin = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Vector2 cursorPos = context.ReadValue<Vector2>();
        RotatePerformed?.Invoke(cursorPos);

        Vector2 direction = cursorPos - CursorOrigin;
        if (direction.sqrMagnitude > 0f)
            direction.Normalize();
        else
            direction = Vector2.zero;

        DirectionFromCursor?.Invoke(direction);
    }

    private void OnRotateCanceled(InputAction.CallbackContext context)
    {
        RotateCanceled?.Invoke(context.ReadValue<Vector2>());
        DirectionFromCursor?.Invoke(Vector2.zero);
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

        _inputActions.UI.ActivateAbility.performed -= OnAbilityUsed;

        _inputActions.Player.Rotate.performed -= OnRotatePerformed;
        _inputActions.Player.Rotate.canceled -= OnRotateCanceled;

        _inputActions.Dispose();
        _inputActions = null;
    }
}
