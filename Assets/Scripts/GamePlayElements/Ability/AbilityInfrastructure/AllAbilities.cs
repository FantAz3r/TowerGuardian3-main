using System.Linq;

public class AllAbilities : AllItems<IAbility, AbilityConfig, AbilityType>
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override AbilityType GetTypeFromConfig(AbilityConfig config) => config.AbilityType;
}
