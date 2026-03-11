public interface IBuffble
{
    void EnableBuff();
    void ApplyBuff(IEffect effect);

    void Recalculate();
    void RemoveBuff(IEffect effect);
}