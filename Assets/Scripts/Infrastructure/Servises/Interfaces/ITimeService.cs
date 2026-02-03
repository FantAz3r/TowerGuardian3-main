public interface ITimeService : IService
{
    bool IsPaused { get; }

    void PauseAll();
    void Pause();
    void PauseForSeconds(float seconds);
    void Resume();
}