using UnityEngine;

public abstract class InteractionMethod : MonoBehaviour
{
    [SerializeField] private bool _canInteract = true;

    private IAction _action;
    private GameUI _gameUI;
    private string _name;

    public virtual void Init(IAction action, GameUI gameUI, string name = "")
    {
        _action = action;
        _gameUI = gameUI;
        _name = name;
    }

    public virtual void EnableInteraction()
    {
        _canInteract = true;
    }

    public virtual void DisableInteraction()
    {
        _canInteract = false;
    }

    public virtual void Interact()
    {
        _action.Execute();
        _gameUI.HUD.Disable();
    }

    public virtual void ResetInteraction() { }
}
