using System.Collections.Generic;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Datas
{
    [CreateAssetMenu(fileName = "ResourceConfig", menuName = "Configs/ResourceConfig")]

    public class ResourceData : ScriptableObject
    {
        public List<ResourceInfo> ResourceInfos = new List<ResourceInfo>();
    }
}