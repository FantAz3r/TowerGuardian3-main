using System;
using System.Collections;

public interface ICooldownAbility : IAbility
{
    bool IsCooldowning { get; }
    float Cooldown { get; }

    event Action<float, float> Cooldowning;
    IEnumerator CooldownRoutine();
}