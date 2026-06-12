using System.Collections.Generic;
using System.Linq;

namespace TowerGuardian.Scripts.GamePlayElements.Buffs.StatsCalculator
{
    public class StatsCalculator
    {
        private List<IEffect> _effects = new List<IEffect>();

        public void AddEffect(IEffect effect)
        {
            _effects.Add(effect);
        }

        public void RemoveEffect(IEffect effect)
        {
            var effectToRemove = _effects.FirstOrDefault(e => e.ID == effect.ID);
            if (effectToRemove != null)
            {
                _effects.Remove(effectToRemove);
            }
        }

        public float Calculate(float @base)
        {
            StatsVisitor visitor = new StatsVisitor(@base);

            foreach (IEffect effect in _effects)
            {
                effect.Calculate(visitor);
            }

            return visitor.Result;
        }

        public int GetEffectsCount() => _effects.Count;
    }
}
