using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Datas", menuName = "Datas/EffectData")]

public class EffectData : ScriptableObject
{
    public List<EffectInfo> EffectInfos = new List<EffectInfo>();

    public EffectInfo GetEffectInfo(EffectType type)
    {
        foreach(var info in EffectInfos)
        {
            if(info.EffectType == type)
                return info;
        }

        return default;
    }
}
