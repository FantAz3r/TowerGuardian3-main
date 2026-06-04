using TowerGuardian.Enums;

public interface IStateSwitchService : IService
{
    void Switch(LevelID state);
}