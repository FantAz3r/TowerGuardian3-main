using UnityEngine;

public abstract class InteractionMethod : MonoBehaviour
{
    [SerializeField] private bool _canInteract = true;
    private IAction _action;

    public void Init(IAction action)
    {
        _action = action;
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
    }

    public virtual void ResetInteraction() { }
}
