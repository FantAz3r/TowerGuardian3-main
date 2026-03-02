using System;
using UnityEngine;
using UnityEngine.InputSystem;
using YG;

public class InputService : IInputService, IAbilityInput
{
    private PlayerInputActions _inputActions;

    public event Action<Vector2> MovePerformed;
    public event Action<Vector2> RotateDirectionSeted;

    public event Action OnAbillity1Used;
    public event Action OnAbillity2Used;
    public event Action OnAbillity3Used;
    public event Action OnAbillity4Used;

    public Vector2 CursorOrigin { get; set; }

    public InputService()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();

        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;

        _inputActions.UI.ActivateAbility1.performed += OnAbility1Used;
        _inputActions.UI.ActivateAbility2.performed += OnAbility2Used;
        _inputActions.UI.ActivateAbility3.performed += OnAbility3Used;
        _inputActions.UI.ActivateAbility4.performed += OnAbility4Used;
        _inputActions.UI.Pause.performed += PauseGame;

        _inputActions.Player.Rotate.performed += OnRotatePerformed;
        _inputActions.Player.Rotate.canceled += OnRotateCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        MovePerformed?.Invoke(direction);
    }

    private void OnAbility1Used(InputAction.CallbackContext context)
    {
        OnAbillity1Used?.Invoke();
    }

    private void OnAbility2Used(InputAction.CallbackContext context)
    {
        OnAbillity2Used?.Invoke();
    }

    private void OnAbility3Used(InputAction.CallbackContext context)
    {
        OnAbillity3Used?.Invoke();
    }

    private void OnAbility4Used(InputAction.CallbackContext context)
    {
        OnAbillity4Used?.Invoke();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MovePerformed?.Invoke(Vector2.zero);
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        if(Time.timeScale != 0)
        {
            ServiceLocator.Get<IWindowService>().Open(WindowType.Pause);
        }
    }

    private void OnRotatePerformed(InputAction.CallbackContext context)
    {
        Vector2 direction = Vector2.zero;

        if (YG2.envir.isDesktop)
        {
            CursorOrigin = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Vector2 cursorPos = context.ReadValue<Vector2>();
            direction = cursorPos - CursorOrigin;
        }

        if (direction.sqrMagnitude > 0f)
            direction.Normalize();
        else
            direction = Vector2.zero;

        RotateDirectionSeted?.Invoke(direction);
    }

    private void OnRotateCanceled(InputAction.CallbackContext context)
    {
        RotateDirectionSeted?.Invoke(Vector2.zero);
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

        _inputActions.UI.ActivateAbility1.performed -= OnAbility1Used;
        _inputActions.UI.ActivateAbility2.performed -= OnAbility2Used;
        _inputActions.UI.ActivateAbility3.performed -= OnAbility3Used;
        _inputActions.UI.ActivateAbility4.performed -= OnAbility4Used;

        _inputActions.Player.Rotate.performed -= OnRotatePerformed;
        _inputActions.Player.Rotate.canceled -= OnRotateCanceled;

        _inputActions.Dispose();
        _inputActions = null;
    }

    public IInputService GetSelf()
    {
        return this;
    }
}
