public interface ITimeService : IService
{
    bool IsPaused { get; }

    void Pause();
    void PauseGame();
    void PauseForSeconds(float seconds);
    void Resume();
}