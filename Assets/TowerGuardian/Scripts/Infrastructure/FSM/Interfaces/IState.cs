namespace TowerGuardian.Scripts.Infrastructure.FSM.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}