using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Items;
using TowerGuardian.Scripts.StaticData.Configs;

namespace TowerGuardian.Scripts.GamePlayElements.Ability.AbilityInfrastructure
{
    public interface IAbility : IItem<AbilityType, AbilityConfig>
    {
    }
}
