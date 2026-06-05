using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.Infrastructure.FSM.Interfaces;
using TowerGuardian.Scripts.Infrastructure.FSM.States;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;

namespace TowerGuardian.Scripts.Infrastructure.Servises
{
    public class StateSwitchService : IStateSwitchService
    {
        private IGameStateMachine _gameStateMachine;

        public StateSwitchService(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Switch(LevelID state)
        {
            _gameStateMachine.EnterIn<LoadingLevelState, LevelID>(state);
        }
    }
}