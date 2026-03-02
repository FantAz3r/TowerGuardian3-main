using UnityEngine;

public abstract class Ability : MonoBehaviour, IAbility
{
    public abstract AbilityType Type { get; }
    public abstract AbilityConfig Config { get; }

    public virtual void Enable()
    {
        enabled = true;
    }

    public virtual void Remove()
    {
        enabled = false;
    }
}
