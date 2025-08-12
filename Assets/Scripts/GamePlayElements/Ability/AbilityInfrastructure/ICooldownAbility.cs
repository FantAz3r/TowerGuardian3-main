using System;
using System.Collections;

public interface ICooldownAbility : IAbility
{
    float Cooldown { get; }

    event Action<float, float> CooldownStarted;
    IEnumerator CooldownRoutine();
}