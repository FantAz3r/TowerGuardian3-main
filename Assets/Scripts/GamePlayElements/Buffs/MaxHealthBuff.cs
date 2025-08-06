public class MaxHealthBuff : IBuff
{
    private Health _health;

    public MaxHealthBuff(Health health)
    {
        _health = health;
    }

    public BuffType Type => BuffType.MaxHp;

    public void ApplyBuff(float value)
    {
        _health.ApplyBuff(value);
    }
}
