using System;

[Serializable]
public struct CardSaveData
{
    public int Level;

    public CardSaveData(int level)
    {
        Level = level;
    }
}