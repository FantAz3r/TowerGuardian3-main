public class RegenerationBuff : Buff
{
    private IBuffble _healthRegeneration;
    public override BuffType Type => BuffType.HpRegen;

    public RegenerationBuff(HealthRegeneration healthRegeneration)
    {
        _healthRegeneration = healthRegeneration;
    }

    public override void Enable()
    {
        _healthRegeneration.EnableBuff();
    }

    public override void Upgrade()
    {
        _healthRegeneration.ApplyBuff(Config.IncreaseValue);
    }

    public override void Remove()
    {
        _healthRegeneration.RemoveBuff();
    }
}
