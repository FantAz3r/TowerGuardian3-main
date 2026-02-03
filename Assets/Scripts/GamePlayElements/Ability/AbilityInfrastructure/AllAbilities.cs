using System.Linq;

public class AllAbilities : AllItems<IAbility, AbilityConfig, AbilityType>
{
    protected override void Awake()
    {
        base.Awake();
        Items = GetComponents<IAbility>().ToList();
    }

    protected override AbilityType GetTypeFromConfig(AbilityConfig config) => config.Type;
}
