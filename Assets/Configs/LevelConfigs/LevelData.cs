using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfigs", menuName = "Configs/LevelConfig")]

public class LevelData : ScriptableObject
{
    public List<LevelInfo> LevelInfos = new List<LevelInfo>(); 
}
