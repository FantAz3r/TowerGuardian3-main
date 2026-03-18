using UnityEngine;

public abstract class Ability : MonoBehaviour, IAbility
{
    public bool IsAbilityActive = false;
    public abstract AbilityType Type { get; }
    public abstract AbilityConfig Config { get; }

    public virtual void Enable()
    {
        IsAbilityActive = true;
        enabled = true;
    }

    public virtual void Remove()
    {
        IsAbilityActive = false;
        enabled = false;
    }
}
