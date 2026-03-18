using UnityEngine;

public class Platform : InteractionMethod
{
    [field: SerializeField] public WindowType WindowType { get; private set; }

    public override void Interact()
    {
        ServiceLocator.Get<IWindowService>().Open(WindowType);
    }
}

