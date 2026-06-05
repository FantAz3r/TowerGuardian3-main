using TowerGuardian.Scripts.Utils;

namespace TowerGuardian.Scripts.GamePlayElements.Buffs.StatsCalculator
{
    public class MultiplyFlatEffect : IEffect
    {
        private readonly int _id;
        private float _value;

        public MultiplyFlatEffect(float value)
        {
            _value = value;
            _id = EffectIdGenerator.GetNextId();
        }

        public int ID => _id;

        public void Calculate(StatsVisitor visitor)
        {
            visitor.AffectFlat(x => x * _value);
        }

        public void UpdateValue(float value)
        {
            _value = value;
        }
    }
}
