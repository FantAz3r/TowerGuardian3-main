using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Datas/SoundData")]
public class SoundData : ScriptableObject
{
    public List<SoundInfo> SoundInfos = new List<SoundInfo>();
    
}
