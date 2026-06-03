public static class EffectIdGenerator
{
    private static int _nextId = 0;

    public static int GetNextId()
    {
        return System.Threading.Interlocked.Increment(ref _nextId);
    }
}

