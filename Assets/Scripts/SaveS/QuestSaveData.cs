using UnityEngine;

public struct QuestSaveData
{
    public LevelID Level;
    public int QuestProgress;
    public int QuestIndex;

    public QuestSaveData(LevelID level, int questProgress, int questIndex)
    {
        Level = level;
        QuestProgress = questProgress;
        QuestIndex = questIndex;
    }
}