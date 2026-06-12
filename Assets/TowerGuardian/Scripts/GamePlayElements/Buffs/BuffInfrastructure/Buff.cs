using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Buffs.StatsCalculator;
using TowerGuardian.Scripts.GamePlayElements.Entity;
using TowerGuardian.Scripts.StaticData.Configs;
using TowerGuardian.Scripts.StaticData.Configs.Interfaces;

namespace TowerGuardian.Scripts.GamePlayElements.Buffs.BuffInfrastructure
{
    public class Buff : IBuff
    {
        private IEffect _effect;

        public Buff(IBuffble buffbleObject, BuffConfig config)
        {
            BuffbleObject = buffbleObject;
            Config = config;

            switch (config.EffectType)
            {
                case BuffEffectType.Additive:
                    _effect = new AddFlatEffect(config.IncreaseValue);
                    break;

                case BuffEffectType.MultiplyFlat:
                    _effect = new MultiplyFlatEffect(config.IncreaseValue);
                    break;

                case BuffEffectType.Exponent:
                    break;

                case BuffEffectType.Multiply:
                    _effect = new MultiplyEffect(config.IncreaseValue);
                    break;
            }
        }

        public IBuffble BuffbleObject { get; private set; }

        public BuffConfig Config { get; private set; }

        public BuffType Type => Config.BuffType;

        public void SetConfig(BuffConfig config)
        {
            Config = config;
        }

        public void Enable()
        {
            BuffbleObject.ApplyBuff(_effect);
            Config.Upgraded += Upgrade;
        }

        public void Upgrade(ICardConfig useles)
        {
            _effect.UpdateValue(Config.IncreaseValue);
            BuffbleObject.Recalculate();
        }

        public void Disable()
        {
            BuffbleObject.RemoveBuff(_effect);
            Config.Upgraded -= Upgrade;
        }
    }
}
