public class RegenerationBuff : IBuff
{
    private HealthRegeneration _healthRegeneration;
    public BuffType Type => BuffType.HpRegen;

    public RegenerationBuff(HealthRegeneration healthRegeneration)
    {
        _healthRegeneration = healthRegeneration;
    }

    public void EnableBuff()
    {
        _healthRegeneration.EnableBuff();
    }

    public void UpdateBuff(float value)
    {
        _healthRegeneration.ApplyBuff(value);
    }
}
