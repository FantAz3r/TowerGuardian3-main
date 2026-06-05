using TowerGuardian.Scripts.GamePlayElements.Buffs.StatsCalculator;

namespace TowerGuardian.Scripts.GamePlayElements.Entity
{
    public interface IBuffble
    {
        void EnableBuff();
        void ApplyBuff(IEffect effect);

        void Recalculate();
        void RemoveBuff(IEffect effect);
    }
}