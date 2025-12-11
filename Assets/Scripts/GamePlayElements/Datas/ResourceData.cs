using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceConfig", menuName = "Configs/ResourceConfig")]

public class ResourceData: ScriptableObject
{
    public List<ResourceInfo> ResourceInfos = new List<ResourceInfo>();
}
