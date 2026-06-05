using System;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.StaticData.Configs;

namespace TowerGuardian.Scripts.StaticData.Structs
{
    [Serializable]
    public struct LevelInfo
    {
        public LevelConfig Config;
        public LevelID LevelID;
    }
}
