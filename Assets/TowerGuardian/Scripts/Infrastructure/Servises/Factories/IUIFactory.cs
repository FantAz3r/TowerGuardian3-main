using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using TowerGuardian.Scripts.UI.Windows;

namespace TowerGuardian.Scripts.Infrastructure.Servises.Factories
{
    public interface IUIFactory : IService
    {
        HUD HUD { get; }
    }
}