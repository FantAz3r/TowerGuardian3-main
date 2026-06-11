using System;

namespace TowerGuardian.Scripts.StaticData.Structs.SaveData
{
    [Serializable]
    public struct LevelSaveData
    {
        public int Level;
        public int Score;
        public int Stars;
        public float Time;
        public bool IsComplite;

        public LevelSaveData(int level, int score, int stars, float time)
        {
            Level = level;
            Score = score;
            Stars = stars;
            Time = time;

            IsComplite = stars >= 1;
        }
    }
}
