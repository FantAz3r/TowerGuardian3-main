using System.Collections.Generic;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Datas
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "Datas/SoundData")]

    public class SoundData : ScriptableObject
    {
        public List<SoundInfo> SoundInfos = new List<SoundInfo>();
    }
}