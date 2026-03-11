public class RegenerationBuff : Buff
{
    public RegenerationBuff(IBuffble buffbleObject) : base(buffbleObject)
    {
    }

    public override BuffType Type => BuffType.HpRegen;

    public override void Enable()
    {
        BuffbleObject.EnableBuff();
        base.Enable();
    }
}
