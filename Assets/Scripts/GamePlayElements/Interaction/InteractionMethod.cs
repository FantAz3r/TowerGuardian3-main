using UnityEngine;

public abstract class InteractionMethod : MonoBehaviour
{
    private IAction _action;
    private GameUI _gameUI;
    private string _name;

    public virtual void Init(IAction action, GameUI gameUI, string name = "")
    {
        _action = action;
        _gameUI = gameUI;
        _name = name;
    }

    public virtual void Interact()
    {
        _action.Execute();
        _gameUI.HUD.Disable();
    }

    public virtual void ResetInteraction() { }
}
