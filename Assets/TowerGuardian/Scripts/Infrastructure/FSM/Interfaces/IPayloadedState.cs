namespace TowerGuardian.Scripts.Infrastructure.FSM.Interfaces
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}