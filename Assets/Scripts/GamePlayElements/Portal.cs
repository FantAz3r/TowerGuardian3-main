using UnityEngine;

public class Portal : BuildingObject
{
    [SerializeField] private LevelID _levelID;
    private GameStateMachine _gameStateMachine;
    private FinishLevelMenu _finishMenu;

    public void Init(GameStateMachine gameStateMachine, FinishLevelMenu finishLMenu)
    {
        _gameStateMachine = gameStateMachine;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _gameStateMachine.EnterIn<LoadingLevelState, LevelID>(_levelID);
        }
    }
}
