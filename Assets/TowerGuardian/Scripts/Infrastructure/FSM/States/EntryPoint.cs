using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.Infrastructure.FSM.States
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Start()
        {
            GameStateMachine stateMachine = new GameStateMachine(this);

            _game = new Game(stateMachine);
            _game.StateMachine.EnterIn<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}