using System;
using UnityEngine;

public abstract class Ability : MonoBehaviour, IAbility
{
    public abstract AbilityType AbilityType { get; }

    public event Action AbilityEnabled;

    public abstract void Upgrade();

    public virtual void Enable()
    {
        enabled = true;
        AbilityEnabled?.Invoke();
    }

    public virtual void Remove()
    {
        enabled = false;
    }
}
