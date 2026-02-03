using UnityEngine;
using UnityEngine.UI;
using YG;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private Toggle _toggleRu;
    [SerializeField] private Toggle _toggleEn;
    [SerializeField] private Toggle _toggleTr;
    [SerializeField] private ToggleGroup _toggleGroup;

    private string _defaultLanguage;
    private string _currentLanguage;

    private void Awake()
    {
        _defaultLanguage = YG2.envir.language;
    }

    private void OnEnable()
    {
        _toggleRu.onValueChanged.AddListener(isOn => OnToggleChanged("ru", isOn));
        _toggleEn.onValueChanged.AddListener(isOn => OnToggleChanged("en", isOn));
        _toggleTr.onValueChanged.AddListener(isOn => OnToggleChanged("tr", isOn));

        LoadLanguage();
        YG2.SwitchLanguage(_currentLanguage);
        SetLanguage(_currentLanguage);
        EnableCurrentToggle();
    }

    private void OnDisable()
    {
        _toggleRu.onValueChanged.RemoveAllListeners();
        _toggleEn.onValueChanged.RemoveAllListeners();
        _toggleTr.onValueChanged.RemoveAllListeners();
        SaveLanguage();
    }

    private void OnToggleChanged(string lang, bool isOn)
    {
        if (isOn == false)
        {
            return;
        }

        if (lang == _currentLanguage)
            return;

        SetLanguage(lang);
    }

    private void EnableCurrentToggle()
    {
        if(_currentLanguage == "ru")
            _toggleRu.isOn = true;
        else if (_currentLanguage == "en")
            _toggleEn.isOn = true;
        else if (_currentLanguage == "tr")
            _toggleTr.isOn = true;
    }

    private void SetLanguage(string lang)
    {
        if (string.IsNullOrEmpty(lang))
            lang = _defaultLanguage;

        if (lang != _currentLanguage)
        {
            _currentLanguage = lang;

            YG2.SwitchLanguage(lang);
            SaveLanguage();
        }
    }

    private void SaveLanguage()
    {
        YG2.saves.Language = _currentLanguage;
        YG2.SaveProgress();
    }

    private void LoadLanguage()
    {
        if (YG2.saves == null)
        {
            _currentLanguage = _defaultLanguage;
            return;
        }

        _currentLanguage = YG2.saves.Language;
    }
}
