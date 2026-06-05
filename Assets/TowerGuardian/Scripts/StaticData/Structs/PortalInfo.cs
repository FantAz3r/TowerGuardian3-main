using System;
using TowerGuardian.Scripts.Enums;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Structs
{
    [Serializable]
    public struct PortalInfo
    {
        public Transform Transform;
        public LevelID LevelID;
        public Material Material;
        public int Floor;
    }
}
