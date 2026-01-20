using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFactory 
{
    private MenuCanvas _uiRoot;
    private ISpawnerService _spawnerService; 
    private IStateSwitchService _stateSwitchService;

    public UIFactory()
    {
        _spawnerService = ServicesLocator.GetService<ISpawnerService>();
        _stateSwitchService = ServicesLocator.GetService<IStateSwitchService>();
    }

    public void CreateFocusController()
    {
        ApplicationFocusController prefab = Resources.Load<ApplicationFocusController>(GameConstants.FocusController);
        ApplicationFocusController focusController = Object.Instantiate(prefab);
    }

    public void CreateUIRoot()
    {
        var scene = SceneManager.GetActiveScene();
        List<GameObject> parentObjects = scene.GetRootGameObjects().ToList();

        foreach (var parent in parentObjects)
        {
            if (parent.TryGetComponent(out MenuCanvas menuCanvas))
            {
                _uiRoot = menuCanvas;
            }
        }
    }

    public void CreateSettings()
    {
        _uiRoot.Settings.gameObject.SetActive(false);
    }

    public void CreateSounds()
    {
        SoundData soundData = Resources.Load<SoundData>(GameConstants.SoundData);
        SoundObject soundObject = Resources.Load<SoundObject>(GameConstants.SoundObject);
        _spawnerService.RegisterSpawner(new SoundSpawner(soundData, soundObject));
    }

    public void CreateBackgroundSounds()
    {
        BackGroundMusic prefab = Resources.Load<BackGroundMusic>(GameConstants.BackGroundMusic);
        BackGroundMusic backGroundMusic = Object.Instantiate(prefab);
    }
}
