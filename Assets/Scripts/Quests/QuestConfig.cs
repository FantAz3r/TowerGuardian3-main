using UnityEngine;

[CreateAssetMenu(fileName = "QuestConfigs", menuName = "Configs/QuestConfig")]
public class QuestConfig : ScriptableObject
{
    [SerializeField] private Sprite _image;
    [SerializeField] private string _description;
    [SerializeField] private QuestType _questType;
    [SerializeField] private int _targetValue;

    public string Description => _description;
    public Sprite Image => _image;
    public QuestType QuestType => _questType;
    public int TargetValue => _targetValue;
}
