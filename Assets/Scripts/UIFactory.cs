using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFactory 
{
    private MenuCanvas _uiRoot;

    private ISpawnerService _spawnerService;
    private IStateSwitchService _stateSwitchService;

    public UIFactory(IStateSwitchService stateSwitchService, ISpawnerService spawnerService)
    {
        _stateSwitchService = stateSwitchService;
        _spawnerService = spawnerService;
    }

    public void CreateUIRoot()
    {
        var scene = SceneManager.GetActiveScene();
        List<GameObject> parentObjects = scene.GetRootGameObjects().ToList();

        foreach (var parent in parentObjects)
        {
            if(parent.TryGetComponent(out MenuCanvas menuCanvas))
            {
                _uiRoot = menuCanvas;
            }
        }
    }

    public void CreateStartButton()
    {
        _uiRoot.StartButton.Init(_stateSwitchService);
    }

    public void CreateSettings()
    {
        _uiRoot.SwichDamageNumbers.Init(_spawnerService);
    }
}
