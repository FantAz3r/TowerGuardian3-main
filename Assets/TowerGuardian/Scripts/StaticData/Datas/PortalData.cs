using System.Collections.Generic;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Datas
{
    [CreateAssetMenu(fileName = "PortalData", menuName = "Datas/PortalData")]

    public class PortalData : ScriptableObject
    {
        public List<PortalInfo> Infos = new List<PortalInfo>();
    }
}