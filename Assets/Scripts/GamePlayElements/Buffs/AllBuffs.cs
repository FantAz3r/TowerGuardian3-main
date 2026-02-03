public class AllBuffs : AllItems<IBuff, BuffConfig, BuffType>
{
    protected override void Awake()
    {
        base.Awake();
        CreateBuffs();
    }

    protected override BuffType GetTypeFromConfig(BuffConfig config)
    {
        return config.BuffType;
    }

    private void CreateBuffs()
    {
        Items.Add(new MaxHealthBuff(Player.Health));
        Items.Add(new SpeedBuff(Player.Mover));
        Items.Add(new CollectRangeBuff(Player.ResourceCollector));
        Items.Add(new RegenerationBuff(Player.HealthRegeneration));
    }
}
