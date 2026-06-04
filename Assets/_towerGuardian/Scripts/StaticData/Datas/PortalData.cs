using System.Collections.Generic;
using UnityEngine;

namespace TowerGuardian.StaticData
{
    [CreateAssetMenu(fileName = "PortalData", menuName = "Datas/PortalData")]

    public class PortalData : ScriptableObject
    {
        public List<PortalInfo> Infos = new List<PortalInfo>();
    }
}