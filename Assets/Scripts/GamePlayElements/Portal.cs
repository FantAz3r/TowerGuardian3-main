using UnityEngine;

public class Portal : BuildingObject
{
    [SerializeField] private LevelID _levelID;
    private WinLevelMenu _finishMenu;

    public void Init( WinLevelMenu finishLMenu)
    {
        _finishMenu = finishLMenu;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            _finishMenu.LevelEnd();
        }
    }
}
