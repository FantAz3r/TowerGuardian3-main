using TowerGuardian.Scripts.Infrastructure.FSM;

namespace TowerGuardian.Scripts.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(GameStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}