using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Items;
using TowerGuardian.Scripts.StaticData.Configs;

namespace TowerGuardian.Scripts.GamePlayElements.Ability.AbilityInfrastructure
{
    public class AllAbilities : AllItems<IAbility, AbilityConfig, AbilityType>
    {
        protected override AbilityType GetTypeFromConfig(AbilityConfig config) => config.AbilityType;
    }
}
