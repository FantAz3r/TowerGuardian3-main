public interface IBuff
{
    BuffType Type { get; }
    void EnableBuff();
    void UpdateBuff(float value);
}
