public class MaxHealthBuff : IBuff
{
    private Health _health;

    public MaxHealthBuff(Health health)
    {
        _health = health;
    }

    public BuffType Type => BuffType.MaxHp;


    public void UpdateBuff(float value)
    {
        _health.ApplyBuff(value);
    }

    public void EnableBuff()
    {
        throw new System.NotImplementedException();
    }
}
