public class SpeedBuff : Buff
{
    private IBuffble _buffbleComponent;

    public SpeedBuff(IBuffble mover) => _buffbleComponent = mover;

    public override BuffType Type => BuffType.MoveSpeed;

    public override void Enable()
    {
        base.Enable();
        _buffbleComponent.ApplyBuff(Config.IncreaseValue);
    }

    public override void Upgrade(ICardConfig useles)
    {
        _buffbleComponent.ApplyBuff(Config.IncreaseValue);
    }

    public override void Remove()
    {
        _buffbleComponent.RemoveBuff();
    }
}
