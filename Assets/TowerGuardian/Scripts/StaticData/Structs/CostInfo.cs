using System;
using TowerGuardian.Scripts.Enums;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Structs
{
    [Serializable]
    public struct CostInfo
    {
        public ResourceType ResourceType;
        public int Value;
        public Sprite Image;

        public CostInfo(ResourceType resourceType, int value, Sprite image = null)
        {
            ResourceType = resourceType;
            Value = value;
            Image = image;
        }
    }
}
