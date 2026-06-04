using TowerGuardian.Enums;

public interface ILevelLoadingService : IService
{
    void Load(LevelID level);
}