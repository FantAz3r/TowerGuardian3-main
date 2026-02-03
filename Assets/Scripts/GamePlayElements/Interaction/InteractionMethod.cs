using UnityEngine;

public abstract class InteractionMethod : MonoBehaviour
{
    private IAction _action;

    public virtual void Init(IAction action)
    {
        _action = action;
    }

    public virtual void Interact()
    {
        _action.Execute();
    }

    public virtual void ResetInteraction() { }
}
