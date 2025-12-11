using System;
using UnityEngine;

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
