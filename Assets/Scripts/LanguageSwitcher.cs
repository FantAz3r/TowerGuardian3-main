using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Insides;

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

        if (string.IsNullOrEmpty(_currentLanguage))
        {
            _currentLanguage = _defaultLanguage;
            SaveLanguage();
        }

        SetLanguage(_currentLanguage);
    }

    private void OnEnable()
    {
        _toggleRu.onValueChanged.AddListener(isOn => OnToggleChanged("ru", isOn));
        _toggleEn.onValueChanged.AddListener(isOn => OnToggleChanged("en", isOn));
        _toggleTr.onValueChanged.AddListener(isOn => OnToggleChanged("tr", isOn));

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
        if (!isOn)
            return;

        if (lang == _currentLanguage)
            return;

        SetLanguage(lang);
    }

    private void EnableCurrentToggle()
    {
        _toggleRu.isOn = _currentLanguage == "ru";
        _toggleEn.isOn = _currentLanguage == "en";
        _toggleTr.isOn = _currentLanguage == "tr";
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
        if (YG2.saves == null)
            return;

        YG2.saves.Language = _currentLanguage;
        YG2.SaveProgress();
    }

    private void LoadLanguage()
    {
        if (YG2.saves == null || string.IsNullOrEmpty(YG2.saves.Language))
        {
            _currentLanguage = null; 
            return;
        }

        _currentLanguage = YG2.saves.Language;
    }
}
