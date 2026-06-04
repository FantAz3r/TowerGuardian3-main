namespace TowerGuardian.Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}