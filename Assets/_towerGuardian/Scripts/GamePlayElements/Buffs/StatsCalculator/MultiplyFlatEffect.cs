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

    public void Effect(StatsVisitor visitor)
    {
        visitor.AffectFlat(x => x * _value);
    }

    public void UpdateValue(float value)
    {
        _value = value;
    }
}
