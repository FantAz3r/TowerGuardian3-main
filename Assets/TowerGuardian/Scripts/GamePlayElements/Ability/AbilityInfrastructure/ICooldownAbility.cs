using System;
using System.Collections;

namespace TowerGuardian.Scripts.GamePlayElements.Ability.AbilityInfrastructure
{
    public interface ICooldownAbility : IAbility
    {
        event Action<float, float> Cooldowning;

        bool IsCooldowning { get; }
        float Cooldown { get; }

        IEnumerator CooldownRoutine();
    }
}