using System.Collections.Generic;
using UnityEngine;

namespace TowerGuardian.StaticData
{
    [CreateAssetMenu(fileName = "Datas", menuName = "Datas/WindowData")]

    public class WindowData : ScriptableObject
    {
        public List<WindowInfo> WindowInfos = new List<WindowInfo>();
    }
}