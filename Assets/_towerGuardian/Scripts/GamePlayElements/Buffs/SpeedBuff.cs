using TowerGuardian.Enums;

public class SpeedBuff : Buff
{
    public SpeedBuff(IBuffble buffbleObject) : base(buffbleObject) { }

    public override BuffType Type => BuffType.MoveSpeed;
}
