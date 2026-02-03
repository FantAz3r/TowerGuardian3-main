using UnityEngine;

public struct QuestSaveData
{
    public LevelID Level;
    public float QuestProgress;
    public float CurrentTime;
    public int QuestIndex;

    public QuestSaveData(LevelID level, float questProgress, float currentTime, int questIndex)
    {
        Level = level;
        QuestProgress = questProgress;
        CurrentTime = currentTime;
        QuestIndex = questIndex;
    }
}