using UnityEngine;

public class Portal : BuildingObject
{
    [SerializeField] private LevelID _levelID;
    private GameStateMachine _gameStateMachine;
    private WinLevelMenu _finishMenu;

    public void Init(GameStateMachine gameStateMachine, WinLevelMenu finishLMenu)
    {
        _gameStateMachine = gameStateMachine;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _finishMenu.LevelEnd();
        }
    }
}
