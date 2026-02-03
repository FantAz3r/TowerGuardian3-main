using System;
using UnityEngine;

public abstract class Ability : MonoBehaviour, IAbility
{
    public abstract AbilityType Type { get; }
    public abstract AbilityConfig Config { get; }

    public event Action AbilityEnabled;

    public virtual void Enable()
    {
    }

    public virtual void Remove()
    {
    }
}
