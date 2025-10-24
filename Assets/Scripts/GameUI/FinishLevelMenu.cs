using UnityEngine;
using UnityEngine.UI;

public class FinishLevelMenu : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;

    private GameStateMachine _gameStateMachine;

    public void Init(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    
}
