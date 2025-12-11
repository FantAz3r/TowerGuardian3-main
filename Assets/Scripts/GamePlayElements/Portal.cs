using System;
using UnityEngine;

public class Portal : BuildingObject
{
    private LevelID _levelID;
    private LevelID _currentLevel;
    private WinLevelMenu _finishMenu;
    private LouseLevelMenu _louseMenu;
    private Material _material;
    private StartLevelMenu _startLevelMenu;

    public event Action Exited;
    public LevelID NextLevel => _levelID;

    private void Awake()
    {
        _material = GetComponent<Material>();
    }

    public void Init(WinLevelMenu finishLMenu, LouseLevelMenu louseMenu, LevelID portalLevelID, Material material, LevelID currentLevel = LevelID.None, StartLevelMenu startLevelMenu = null)
    {
        _finishMenu = finishLMenu;
        _louseMenu = louseMenu;
        _material = material;
        _currentLevel = currentLevel;
        _levelID = portalLevelID;
        _startLevelMenu = startLevelMenu;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            if (_currentLevel == LevelID.Tower)
            {
                _startLevelMenu.SetPortalLevel(_levelID);
            }
            else
            {
                _finishMenu.LevelEnd(_levelID);
            }

            Exited?.Invoke();
        }
    }
}
