public interface IBuff
{
    BuffType Type { get; }
    void ApplyBuff(float value);
}
