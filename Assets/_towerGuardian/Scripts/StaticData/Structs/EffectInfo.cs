using System;
using TowerGuardian.Enums;
using UnityEngine;

[Serializable]
public struct EffectInfo
{
    public Effect Prefab;
    public EffectType EffectType;
    public Vector3 Offset;
}