using System;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects;

namespace TowerGuardian.Scripts.StaticData.Structs
{
    [Serializable]
    public struct ResourceInfo
    {
        public ResourceType Type;
        public ResourcePiece Prefab;
    }
}
