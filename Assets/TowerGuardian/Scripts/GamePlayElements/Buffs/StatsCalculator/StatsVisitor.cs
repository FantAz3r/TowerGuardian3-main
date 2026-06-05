using System;

namespace TowerGuardian.Scripts.GamePlayElements.Buffs.StatsCalculator
{
    public class StatsVisitor
    {
        private float _base;
        private float _multiplier = 1;
        private float _flat;

        public StatsVisitor(float @base)
        {
            _base = @base;
        }

        public float Result => (_base + _flat) * _multiplier;

        public void AffectMultiplier(Func<float, float> func) =>
            _multiplier = func.Invoke(_multiplier);

        public void AffectFlat(Func<float, float> func) =>
            _flat = func.Invoke(_flat);
    }
}
