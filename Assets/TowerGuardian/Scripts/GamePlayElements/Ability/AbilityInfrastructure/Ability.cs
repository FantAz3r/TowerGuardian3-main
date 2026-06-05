using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.StaticData.Configs;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Ability.AbilityInfrastructure
{
    public abstract class Ability : MonoBehaviour, IAbility
    {
        public bool IsAbilityActive;
        public abstract AbilityType Type { get; }
        public abstract AbilityConfig Config { get; }

        public virtual void Enable()
        {
            IsAbilityActive = true;
            enabled = true;
        }

        public virtual void Disable()
        {
            IsAbilityActive = false;
            enabled = false;
        }
    }
}
