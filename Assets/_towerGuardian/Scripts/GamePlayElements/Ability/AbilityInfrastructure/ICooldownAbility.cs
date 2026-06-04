using System;
using System.Collections;

public interface ICooldownAbility : IAbility
{
    event Action<float, float> Cooldowning;

    bool IsCooldowning { get; }
    float Cooldown { get; }

    IEnumerator CooldownRoutine();
}