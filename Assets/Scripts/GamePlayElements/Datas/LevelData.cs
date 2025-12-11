using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Datas", menuName = "Datas/LevelData")]

public class LevelData : ScriptableObject
{
    public List<LevelInfo> LevelInfos = new List<LevelInfo>(); 
}
