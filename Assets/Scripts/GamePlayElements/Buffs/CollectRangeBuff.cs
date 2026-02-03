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
    }

    public override void Upgrade()
    {
        _buffbleObject.ApplyBuff(Config.IncreaseValue);
    }

    public override void Remove()
    {
        _buffbleObject.RemoveBuff();
    }
}
