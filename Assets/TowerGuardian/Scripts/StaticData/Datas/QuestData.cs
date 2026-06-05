using System.Collections.Generic;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Datas
{
    [CreateAssetMenu(fileName = "Datas", menuName = "Datas/QuestData")]

    public class QuestData : ScriptableObject
    {
        public List<QuestInfo> QuestInfos = new List<QuestInfo>();
    }
}