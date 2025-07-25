public interface ITimeService : IService
{
    bool IsPaused { get; }

    void Pause();
    void PauseForSeconds(float seconds);
    void Resume();
}