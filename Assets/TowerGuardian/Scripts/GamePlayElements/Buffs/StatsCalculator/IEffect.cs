namespace TowerGuardian.Scripts.GamePlayElements.Buffs.StatsCalculator
{
    public interface IEffect
    {
        int ID { get; }

        void Calculate(StatsVisitor visitor);

        void UpdateValue(float value);
    }
}
