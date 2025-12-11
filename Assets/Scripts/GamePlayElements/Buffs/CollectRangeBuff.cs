using UnityEngine;

public class CollectRangeBuff : IBuff
{
    private ResourceCollector _collector;

    public CollectRangeBuff(ResourceCollector collector)
    {
        _collector = collector;
    }

    public BuffType Type => BuffType.CollectRange;

    public void ApplyBuff(float value)
    {
        _collector.ApplyBuff(value);
    }
}
