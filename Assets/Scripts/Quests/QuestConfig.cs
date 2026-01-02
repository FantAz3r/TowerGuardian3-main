using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "QuestConfigs", menuName = "Configs/QuestConfig")]
public class QuestConfig : ScriptableObject
{
    [SerializeField] private Sprite _image;
    [SerializeField] private string _descriptionRU;
    [SerializeField] private string _descriptionEN;
    [SerializeField] private string _descriptionTR;
    [SerializeField] private QuestType _questType;
    [SerializeField] private int _targetValue;

    public string Description => OnCorrectLanguage(_descriptionRU, _descriptionEN, _descriptionTR);
    public Sprite Image => _image;
    public QuestType QuestType => _questType;
    public int TargetValue => _targetValue;

    private string OnCorrectLanguage(string ru, string en, string tr)
    {
        string lang = YG2.lang;

        switch (lang)
        {
            case "ru":
                return ru;
            case "en":
                return en;
            case "tr":
                return tr;
            default:
                return "";
        }
    }
}
