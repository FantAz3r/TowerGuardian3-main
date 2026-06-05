using TowerGuardian.Scripts.Enums;

namespace TowerGuardian.Scripts.Infrastructure.Servises.Interfaces
{
    public interface ILevelLoadingService : IService
    {
        void Load(LevelID level);
    }
}