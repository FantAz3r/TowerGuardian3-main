public abstract class Buff : IBuff
{
    public BuffConfig Config { get; private set; }
    public abstract BuffType Type { get; }

    public void SetConfig(BuffConfig config) => Config = config;

    public virtual void Enable()
    {
        Config.Upgraded += Upgrade;
    }

    public virtual void Upgrade()
    {

    }

    public virtual void Remove()
    {
        Config.Upgraded -= Upgrade;
    }
}
