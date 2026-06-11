using System;

namespace TowerGuardian.Scripts.StaticData.Structs.SaveData
{
    [Serializable]
    public struct QuestSaveData
    {
        public int Level;
        public float QuestProgress;
        public float CurrentTime;
        public int QuestIndex;

        public QuestSaveData(int level, float questProgress, float currentTime, int questIndex)
        {
            Level = level;
            QuestProgress = questProgress;
            CurrentTime = currentTime;
            QuestIndex = questIndex;
        }
    }
}