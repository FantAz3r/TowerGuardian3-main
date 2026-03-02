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
        base.Enable();
        _healthRegeneration.EnableBuff();
        _healthRegeneration.ApplyBuff(Config.IncreaseValue);
    }

    public override void Upgrade(ICardConfig useles)
    {
        _healthRegeneration.ApplyBuff(Config.IncreaseValue);
    }

    public override void Remove()
    {
        base.Remove();
        _healthRegeneration.RemoveBuff();
    }
}
