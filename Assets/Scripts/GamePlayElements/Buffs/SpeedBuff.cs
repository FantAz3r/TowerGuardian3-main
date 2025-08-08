public class SpeedBuff : IBuff
{
    private Mover _mover;

    public SpeedBuff(Mover mover)
    {
        _mover = mover;
    }

    public BuffType Type => BuffType.MoveSpeed;

    public void ApplyBuff(float value)
    {
        _mover.ApplyBuff(value);
    }
}
