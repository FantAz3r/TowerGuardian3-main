using TowerGuardian.Enums;
using TowerGuardian.StaticData;

public class AllAbilities : AllItems<IAbility, AbilityConfig, AbilityType>
{
    protected override AbilityType GetTypeFromConfig(AbilityConfig config) => config.AbilityType;
}
