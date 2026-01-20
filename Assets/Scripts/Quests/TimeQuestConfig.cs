
using UnityEngine;

[CreateAssetMenu(fileName = "TimeQuestConfig", menuName = "Configs/TimeQuestConfig")]
public class TimeQuestConfig : QuestConfig
{
    [SerializeField] private float _timeLimit;

    public float TimeLimit => _timeLimit;
}
