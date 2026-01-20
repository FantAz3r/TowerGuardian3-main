using System;
using UnityEngine;

public class Portal : BuildingObject
{
    [SerializeField] private MeshRenderer _materialInner;
    [SerializeField] private MeshRenderer _materialOuter; 

    private LevelID _levelID;
    private LevelID _currentLevel;
    private WinLevelMenu _winMenu;
    private LouseLevelMenu _louseMenu;
    private StartLevelMenu _startLevelMenu;

    public event Action EnemyEntered;
    public event Action Entered;
    public LevelID NextLevel => _levelID;

    public void Init(WinLevelMenu finishLMenu, LouseLevelMenu louseMenu, LevelID portalLevelID, Material material, LevelID currentLevel = LevelID.None, StartLevelMenu startLevelMenu = null)
    {
        _winMenu = finishLMenu;
        _louseMenu = louseMenu;
        _currentLevel = currentLevel;
        _levelID = portalLevelID;
        _startLevelMenu = startLevelMenu;

        _materialInner.material = material;
        _materialInner.material = material;
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
                _winMenu.LevelEnd(_levelID);
            }

            Entered?.Invoke();
        }

        if(other.TryGetComponent(out Enemy enemy))
        {
            EnemyEntered?.Invoke();
            enemy.gameObject.SetActive(false);
        }
    }
}
