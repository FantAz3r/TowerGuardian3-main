using UnityEngine;

public class UIFactory 
{
    private Transform _uiRoot;
    private Transform _menuPanel;
    private Transform _settingsPanel;
    private IStateSwitchService _stateSwitchService;
    public UIFactory(IStateSwitchService stateSwitchService)
    {
        _stateSwitchService = stateSwitchService;
    }

    public void CreateUIRoot()
    {
        GameObject prefab = Resources.Load<GameObject>(GameConstants.MenuCanvas);
        _uiRoot = Object.Instantiate(prefab).transform;
    }

    public void CreateStartButton()
    {
        GameObject prefab = Resources.Load<GameObject>(GameConstants.StartButton);
        Transform container = _uiRoot.GetComponentInChildren<UIDummy>().transform;
        GameObject button = Object.Instantiate(prefab, container);

        button.GetComponent<StartButton>().Init(_stateSwitchService);
        button.transform.SetSiblingIndex(0);
    }
}
