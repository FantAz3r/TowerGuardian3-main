public interface ITimeService : IService
{
    bool IsPaused { get; }

    void SmoothEditTimeScalse(float targetTimeScale, float duration);
}