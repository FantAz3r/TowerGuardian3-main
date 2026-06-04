using System.Collections.Generic;
using UnityEngine;

namespace TowerGuardian.StaticData
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "Datas/SoundData")]

    public class SoundData : ScriptableObject
    {
        public List<SoundInfo> SoundInfos = new List<SoundInfo>();
    }
}