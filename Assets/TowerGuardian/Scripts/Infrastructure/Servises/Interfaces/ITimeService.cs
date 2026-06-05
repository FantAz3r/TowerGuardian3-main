namespace TowerGuardian.Scripts.Infrastructure.Servises.Interfaces
{
    public interface ITimeService : IService
    {
        bool IsPaused { get; }
        void StopGame();
        void ResumeGame();
        void SmoothEditTimeScalse(float targetTimeScale, float duration);
    }
}