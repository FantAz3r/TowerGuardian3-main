using TowerGuardian.Scripts.Enums;

namespace TowerGuardian.Scripts.Infrastructure.Servises.Interfaces
{
    public interface IStateSwitchService : IService
    {
        void Switch(LevelID state);
    }
}