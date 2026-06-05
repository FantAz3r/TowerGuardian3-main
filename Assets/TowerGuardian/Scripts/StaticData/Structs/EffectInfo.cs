using System;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Effects;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Structs
{
    [Serializable]
    public struct EffectInfo
    {
        public Effect Prefab;
        public EffectType EffectType;
        public Vector3 Offset;
    }
}