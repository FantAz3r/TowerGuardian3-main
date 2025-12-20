using System;

[Serializable]
public struct LevelSaveData 
{
    public LevelID Level;
    public int Score;
    public int Stars;
    public int Time;
    public bool IsComplite;

    public LevelSaveData(LevelID level, int score, int stars, int time)
    {
        Level = level;
        Score = score;
        Stars = stars;
        Time = time;

        IsComplite = stars >= 1;
    }
}
