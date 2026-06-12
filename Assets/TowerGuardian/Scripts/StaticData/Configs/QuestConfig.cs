using TowerGuardian.Scripts.Enums;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.StaticData.Configs
{
    [CreateAssetMenu(fileName = "QuestConfigs", menuName = "Configs/QuestConfig")]

    public class QuestConfig : ScriptableObject
    {
        [field: SerializeField]
        public Sprite Image { get; private set; }

        [field: SerializeField]
        public string DescriptionRU { get; private set; }

        [field: SerializeField]
        public string DescriptionEN { get; private set; }

        [field: SerializeField]
        public string DescriptionTR { get; private set; }

        [field: SerializeField]
        public string MobileDescriptionRU { get; private set; }

        [field: SerializeField]
        public string MobileDescriptionEN { get; private set; }

        [field: SerializeField]
        public string MobileDescriptionTR { get; private set; }

        [field: SerializeField]
        public QuestType QuestType { get; private set; }

        [field: SerializeField]
        public bool IsProgressQuest { get; private set; }

        [field: SerializeField]
        public int TargetValue { get; private set; }

        [field: SerializeField]
        public bool IsTimeQuest { get; private set; }

        [field: SerializeField]
        public float TimeLimit { get; private set; }

        [field: SerializeField]
        public int ScorePoints { get; private set; }

        public string MobileDescription => OnCorrectLanguage(MobileDescriptionRU, MobileDescriptionEN, MobileDescriptionTR);

        public string Description => OnCorrectLanguage(DescriptionRU, DescriptionEN, DescriptionTR);

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
                    return string.Empty;
            }
        }
    }
}