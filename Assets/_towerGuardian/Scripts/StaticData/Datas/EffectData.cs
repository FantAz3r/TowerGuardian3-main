using System.Collections.Generic;
using TowerGuardian.Enums;
using UnityEngine;

namespace TowerGuardian.StaticData
{
    [CreateAssetMenu(fileName = "Datas", menuName = "Datas/EffectData")]

    public class EffectData : ScriptableObject
    {
        public List<EffectInfo> EffectInfos = new List<EffectInfo>();

        public EffectInfo GetEffectInfo(EffectType type)
        {
            foreach (var info in EffectInfos)
            {
                if (info.EffectType == type)
                    return info;
            }

            return default;
        }
    }
}