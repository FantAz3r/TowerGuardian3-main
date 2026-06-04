using System;

[Serializable]
public struct CardSaveData
{
    public string ID;
    public int Level;
    public bool IsBought;
    public bool HasPlayer;

    public CardSaveData(int level, string id, bool isBought = false, bool hasPlayer = false)
    {
        Level = level;
        ID = id;
        IsBought = isBought;
        HasPlayer = hasPlayer;
    }
}