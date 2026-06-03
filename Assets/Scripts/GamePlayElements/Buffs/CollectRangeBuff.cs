public class CollectRangeBuff : Buff
{
    public CollectRangeBuff(IBuffble buffbleObject) : base(buffbleObject) { }
    public override BuffType Type => BuffType.CollectRange;
}
