public interface IEffect
{
    int ID { get; }

    public void Effect(StatsVisitor visitor);
    void UpdateValue(float value);
}
