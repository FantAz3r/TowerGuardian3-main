using TMPro;
using UnityEngine;
using YG;

public class LanguageSwitch : MonoBehaviour
{
    [SerializeField] private string _ru;
    [SerializeField] private string _en;
    [SerializeField] private string _tr;

    private TMP_Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
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
                textComponent.text = _ru;
                break;
            case "tr":
                textComponent.text = _tr;
                break;
            default:
                textComponent.text = _en;
                break;
        }
    }
}
