using System.Collections.Generic;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Datas
{
    [CreateAssetMenu(fileName = "Datas", menuName = "Datas/WindowData")]

    public class WindowData : ScriptableObject
    {
        public List<WindowInfo> WindowInfos = new List<WindowInfo>();
    }
}