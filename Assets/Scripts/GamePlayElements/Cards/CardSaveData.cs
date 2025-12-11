using System;

[Serializable]
public struct CardSaveData
{
    public string Name;
    public int Level;
    public bool IsBought;
    public bool HasPlayer;

    public CardSaveData(int level, string name, bool isBought = false, bool hasPlayer = false)
    {
        Level = level;
        Name = name;
        IsBought = isBought;
        HasPlayer = hasPlayer;
    }
}