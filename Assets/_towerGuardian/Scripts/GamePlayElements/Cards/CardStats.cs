public struct CardStats 
{
    public string Name;
    public float Value;
    public float NextValue;

    public CardStats(string name, float value, float nextValue)
    {
        Name = name;
        Value = value;
        NextValue = nextValue;
    }
}
