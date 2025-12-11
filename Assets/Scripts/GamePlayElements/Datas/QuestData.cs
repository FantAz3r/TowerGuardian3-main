using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Datas", menuName = "Datas/QuestData")]

public class QuestData : ScriptableObject
{
   public List<QuestInfo> QuestInfos = new List<QuestInfo>();
}
