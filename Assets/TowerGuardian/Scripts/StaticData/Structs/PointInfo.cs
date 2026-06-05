using System;
using TowerGuardian.Scripts.Enums;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Structs
{
    [Serializable]
    public struct PointInfo
    {
        public LevelID PreviousLevel;
        public Transform SpawnPoint;
    }
}