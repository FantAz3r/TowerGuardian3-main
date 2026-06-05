using System.Collections.Generic;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Datas
{
    [CreateAssetMenu(fileName = "Datas", menuName = "Datas/LevelData")]

    public class LevelData : ScriptableObject
    {
        public List<LevelInfo> LevelInfos = new List<LevelInfo>();
    }
}