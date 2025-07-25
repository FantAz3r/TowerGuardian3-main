using UnityEngine;

public class EntryPoint : MonoBehaviour , ICoroutineRunner
{
    [SerializeField] private LoadingScreen _loadingScreen;

    private Game _game;

    private void Awake()
    {
        GameStateMachine stateMachine = new GameStateMachine(new SceneLoader(this), _loadingScreen, new AllServices(), this);

        _game = new Game(stateMachine);
        _game.StateMachine.EnterIn<BootstrapState>();

        DontDestroyOnLoad(this);
    }
}
