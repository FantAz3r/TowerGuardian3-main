using System;
using TowerGuardian.Enums;
using UnityEngine;

[Serializable]
public struct PortalInfo 
{
    public Transform Transform;
    public LevelID LevelID;
    public Material Material;
    public int Floor;
}
