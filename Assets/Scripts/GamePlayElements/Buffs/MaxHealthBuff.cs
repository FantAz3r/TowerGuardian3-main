public class MaxHealthBuff : Buff
{
    private IBuffble _buffbleComponent;

    public MaxHealthBuff(Health health)
    {
        _buffbleComponent = health;
    }

    public override BuffType Type => BuffType.MaxHp;


    public override void Enable()
    {
        _buffbleComponent.ApplyBuff(Config.IncreaseValue);
    }

    public override void Upgrade()
    {
        _buffbleComponent.ApplyBuff(Config.IncreaseValue);
    }

    public override void Remove()
    {
        _buffbleComponent.RemoveBuff();
    }
}
