public class CollectRangeBuff : Buff
{
    private IBuffble _buffbleObject;

    public CollectRangeBuff(IBuffble collector)
    {
        _buffbleObject = collector;
    }

    public override BuffType Type => BuffType.CollectRange;

    public override void Enable()
    {
        base.Enable();
        _buffbleObject.ApplyBuff(Config.IncreaseValue);
    }

    public override void Upgrade(ICardConfig useles)
    {
        _buffbleObject.ApplyBuff(Config.IncreaseValue);
    }

    public override void Remove()
    {
        base.Remove();
        _buffbleObject.RemoveBuff();
    }
}
