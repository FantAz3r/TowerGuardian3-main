public abstract class Buff : IBuff
{
    private IEffect _effect;

    public Buff(IBuffble buffbleObject)
    {
        BuffbleObject = buffbleObject;
    }

    public IBuffble BuffbleObject { get; private set; }
    public BuffConfig Config { get; private set; }
    public abstract BuffType Type { get; }

    public virtual void SetConfig(BuffConfig config)
    {
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

    public virtual void Enable()
    {
        BuffbleObject.ApplyBuff(_effect);
        Config.Upgraded += Upgrade;
    }

    public virtual void Upgrade(ICardConfig useles)
    {
        _effect.UpdateValue(Config.IncreaseValue);
        BuffbleObject.Recalculate();
    }

    public virtual void Remove()
    {
        BuffbleObject.RemoveBuff(_effect);
        Config.Upgraded -= Upgrade;
    }
}
