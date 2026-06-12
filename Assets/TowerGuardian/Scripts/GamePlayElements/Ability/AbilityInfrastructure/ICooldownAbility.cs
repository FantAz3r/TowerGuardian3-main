using System;

namespace TowerGuardian.Scripts.GamePlayElements.Ability.AbilityInfrastructure
{
    public interface ICooldownAbility : IAbility
    {
        event Action<float, float> Cooldowning;

        float Cooldown { get; }
    }
}