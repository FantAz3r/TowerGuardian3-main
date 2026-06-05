using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.Infrastructure.FSM.Interfaces;
using TowerGuardian.Scripts.Infrastructure.FSM.States;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;

namespace TowerGuardian.Scripts.Infrastructure.Servises
{
    public class LevelLoadingService : ILevelLoadingService
    {
        private readonly IGameStateMachine _stateMachine;

        public LevelLoadingService(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Load(LevelID level) => _stateMachine.EnterIn<LoadingLevelState, LevelID>(level);
    }
}