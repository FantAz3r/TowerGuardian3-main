public class SpeedBuff : IBuff
{
    private Mover _mover;

    public SpeedBuff(Mover mover)
    {
        _mover = mover;
    }

    public BuffType Type => BuffType.MoveSpeed;

    public void UpdateBuff(float value)
    {
        _mover.ApplyBuff(value);
    }

    public void EnableBuff()
    {
    }
}
