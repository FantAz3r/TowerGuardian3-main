namespace TowerGuardian.Factories
{
    public interface IUIFactory : IService
    {
        HUD HUD { get; }
    }
}