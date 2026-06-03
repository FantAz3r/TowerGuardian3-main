public class MaxHealthBuff : Buff
{
    public MaxHealthBuff(IBuffble buffbleObject) : base(buffbleObject) { }

    public override BuffType Type => BuffType.MaxHp;
}
