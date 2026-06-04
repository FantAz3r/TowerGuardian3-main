using UnityEngine;

namespace TowerGuardian.Infrastructure
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Start()
        {
            GameStateMachine stateMachine = new GameStateMachine(new SceneLoader(this), this);

            _game = new Game(stateMachine);
            _game.StateMachine.EnterIn<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}