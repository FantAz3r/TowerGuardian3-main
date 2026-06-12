using TMPro;
using UnityEngine;
using YG;

namespace TowerGuardian.Scripts.Localization
{
    public class LanguageSwitch : MonoBehaviour
    {
        [SerializeField]
        private string _ru;
        [SerializeField]
        private string _en;
        [SerializeField]
        private string _tr;

        private TMP_Text _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            YG2.onSwitchLang += SwitchLanguage;
            SwitchLanguage(YG2.lang);
        }

        private void OnDisable()
        {
            YG2.onSwitchLang -= SwitchLanguage;
        }

        public void SwitchLanguage(string lang)
        {
            switch (lang)
            {
                case "ru":
                    _textComponent.text = _ru;
                    break;
                case "tr":
                    _textComponent.text = _tr;
                    break;
                default:
                    _textComponent.text = _en;
                    break;
            }
        }
    }
}
