using System;
using TowerGuardian.Scripts.Enums;

namespace TowerGuardian.Scripts.StaticData.Structs.SaveData
{
    [Serializable]
    public struct LevelSaveData
    {
        public LevelID Level;
        public int Score;
        public int Stars;
        public float Time;
        public bool IsComplite;

        public LevelSaveData(LevelID level, int score, int stars, float time)
        {
            Level = level;
            Score = score;
            Stars = stars;
            Time = time;

            IsComplite = stars >= 1;
        }
    }
}
